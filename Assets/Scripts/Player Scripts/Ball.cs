using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // SerializeField will expose the field to the inspector without changing its visibility.
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float speed = 250;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    private float xInput, zInput, distToGround;
    [SerializeField] private float upJumpForce = 5f;
    [SerializeField] private float brakeMultiplier = 2f;
    Rigidbody ballRigidbody;
    private bool spacePressed, shiftPressed;
    
    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 moveDirection;

    public Transform thirdPersonCam;


    // Use this for initialization
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    // Note - here is where you get all your inputs, check em, etc.
    void Update()
    {
        CalculatePlayerMovement();

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
        // "deathplane" is below y -110
        if (transform.position.y <= -110)
        {
            ResetPlayer();
        }

        MovePlayer();

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
            PlayerStats.Points += 1;
        }
    }

    // Move the player back to a location pre-defined as the reset location
    // zeroing out their movement as well!
    private void ResetPlayer()
    {
        transform.position = new Vector3(0, 1, 0);
        ballRigidbody.velocity = new Vector3(0, 0, 0);
    }

    private void CalculatePlayerMovement()
    {
        // Here we're reading the horizontal input from the InputManager
        // The settings for this are defined in the Project Settings -> Input
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        // this will be the direction we move in relative to the input
        Vector3 direction = new Vector3(xInput, 0f, zInput);

        // We then transform this based on the current direction of the 
        // camera, so we move in that direction!
        moveDirection = thirdPersonCam.TransformDirection(direction);
    }

    // Within here, apply any movement to the player that has been calculated.
    private void MovePlayer()
    {
        // if we're grounded apply a speed force in the direction we're pointing with the inputs checked (keyboard)
        if (IsGrounded())
        {
            // if we're grounded apply a speed force in the direction we're pointing with the inputs checked (keyboard)
            ballRigidbody.AddForce(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            // if we're not grounded we only want a little air control, not full air control
            ballRigidbody.AddForce(moveDirection * speed / 2 * Time.deltaTime);
        }

        // breaking - apply the equal opposite force multiplied by the breakMultiplier to the ball
        if (shiftPressed)
        {
            ballRigidbody.AddForce(new Vector3(0 - ballRigidbody.velocity.x * brakeMultiplier, 0, 0 - ballRigidbody.velocity.z * brakeMultiplier) * speed * Time.deltaTime);
        }

        // jump when the space button is pressed
        if (spacePressed && IsGrounded())
        {
            ballRigidbody.AddForce(Vector3.up * upJumpForce, ForceMode.VelocityChange);
            spacePressed = false;
        }

        // fancy complicated jump fall stuff. creates nicer feeling arcs on the way down. 
        // also allows for holding down space to go a weeeee bit higher
        if (ballRigidbody.velocity.y < 0)
        {
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (ballRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // a really neat raycast downwards that allows for very quick checks
    // of whether you're grounded or not
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
