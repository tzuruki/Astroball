using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // SerializeField will expose the field to the inspector without changing its visibility.
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool useForce, useVelocity;
    [SerializeField] private float speed = 250;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    private float xInput, zInput, distToGround;
    Rigidbody ballRigidbody;
    private bool isGrounded, spacePressed, shiftPressed;


    // Use this for initialization
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        useForce = true;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    // Note - here is where you get all your inputs, check em, etc.
    void Update()
    {
        // Here we're reading the horizontal input from the InputManager
        // The settings for this are defined in the Project Settings -> Input
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftPressed = false;
        }
    }

    // FixedUpdate is called once every physics update (100x a s)
    // Note - here is where you act upon any inputs you've read
    void FixedUpdate()
    {



        if (useVelocity)
        {
            // Here we're applying the horizontal input we checked in the Update method.
            ballRigidbody.velocity = new Vector3(xInput, ballRigidbody.velocity.y, zInput) * speed;
        }

        if (useForce)
        {
            ballRigidbody.AddForce(new Vector3(xInput, 0, zInput) * speed * Time.deltaTime);
        }

        if (shiftPressed)
        {
            ballRigidbody.AddForce(new Vector3(0 - ballRigidbody.velocity.x * 2, 0, 0 - ballRigidbody.velocity.z * 2) * speed * Time.deltaTime);
        }

        // jump when the spcae button is pressed
        if (spacePressed && IsGrounded())
        {
            ballRigidbody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            spacePressed = false;
        }

        if (ballRigidbody.velocity.y < 0)
        {
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (ballRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }



    // Here this method will enter when our object enters a trigger
    // We defined a trigger in our coin object, so that'll populate the "other"
    // input
    private void OnTriggerEnter(Collider other)
    {
        // We then want to check what layer we've assigned our object (after accessing)
        // so that we can verify its one we wanted to use right?
        if (other.gameObject.layer == 9)
        {
            // kill it kill it dead
            Destroy(other.gameObject);
        }
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
