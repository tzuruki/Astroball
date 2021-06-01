using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // SerializeField will expose the field to the inspector without changing its visibility.
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool useForce, useVelocity;
    [SerializeField] private int collisionLayers;
    private float xInput, zInput;
    Rigidbody ballComponent;
    private bool isGrounded, spacePressed;
 

    // Use this for initialization
    void Start()
    {
        ballComponent = GetComponent<Rigidbody>();
        useForce = true;
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
    }

    // FixedUpdate is called once every physics update (100x a s)
    // Note - here is where you act upon any inputs you've read
    void FixedUpdate()
    {

        if (useVelocity)
        {
            // Here we're applying the horizontal input we checked in the Update method.
            ballComponent.velocity = new Vector3(xInput, ballComponent.velocity.y, zInput);
        }

        if(useForce)
        {
            ballComponent.AddForce(new Vector3(xInput, 0, zInput));
        }

        if(Physics.OverlapSphere(groundCheckTransform.position, 1f, playerMask).Length != 0)
            Debug.Log("we're colliding");

        // What this is doing is checking for how many things are colliding with the transform
        // that we've created above, in a sphere of 0.1f units. Thats the thing we stuck on the
        // arse of the ball!
        if (Physics.OverlapSphere(groundCheckTransform.position, 1f, playerMask).Length == 0)
        {
            return;
        }

        // jump when the spcae button is pressed
        if(spacePressed)
        {
            ballComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            spacePressed = false;
        }


    }

    

    // Here this method will enter when our object enters a trigger
    // We defined a trigger in our coin object, so that'll populate the "other"
    // input
    private void OnTriggerEnter(Collider other)
    {
        // We then want to check what layer we've assigned our object (after accessing)
        // so that we can verify its one we wanted to use right?
        if(other.gameObject.layer == 9)
        {
            // kill it kill it dead
            Destroy(other.gameObject);
        }
    }

}
