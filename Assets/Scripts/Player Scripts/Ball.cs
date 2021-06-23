using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;

public class Ball : MonoBehaviour
{
    // SerializeField will expose the field to the inspector without changing its visibility.
    [SerializeField] private float speed = 250;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float upJumpForce = 5f;
    [SerializeField] private float brakeMultiplier = 2f;
    [SerializeField] private float maxDrag = 2f;
    [SerializeField] private float forceConstant = 400f;
    [SerializeField] private GameObject debugTextField;

    Rigidbody ballRigidbody;
    bool spacePressed, shiftPressed, changedDirection;
    Vector3 moveDirection, xMoveDirection;
    public Transform thirdPersonCam;
    private float xInput, zInput, xInputPrev, distToGround;
    Text debugText;
    int numTimesXChanged = 0;

    // Use this for initialization
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        ballRigidbody.maxAngularVelocity = 25f;
        debugText = debugTextField.GetComponent<Text>();
        PlayerStats.Health = 3;
        xInputPrev = 0;
    }

    // Update is called once per frame
    // Note - here is where you get all your inputs, check em, etc.
    void Update()
    {


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

        Debug.Log(ballRigidbody.drag);
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

        CalculatePlayerMovement();

        MovePlayer();
    }



    // Here this method will enter when our object enters a trigger
    // We defined a trigger in our coin object, so that'll populate the "other"
    // input
    private void OnTriggerEnter(Collider other)
    {
        // We then want to check what layer we've assigned our object (after accessing)
        // so that we can verify its one we wanted to use right?

        // Layer 9 is collectables for points
        if (other.gameObject.layer == 9)
        {
            // kill it kill it dead
            Destroy(other.gameObject);
            PlayerStats.Points += 1;
        }

        // Keys are on layer 10
        if (other.gameObject.layer == 10)
        {

            KeyScript key = other.gameObject.GetComponent<KeyScript>();

            DoorKey tempKey = new DoorKey();

            tempKey.colour = key.GetColour();
            tempKey.level = key.GetLevel();

            PlayerStats.AddKey(tempKey);

            Destroy(other.gameObject);
        }


    }

    // Move the player back to a location pre-defined as the reset location
    // zeroing out their movement as well!
    private void ResetPlayer()
    {
        transform.localPosition = new Vector3(0, 1, 0);
        ballRigidbody.velocity = new Vector3(0, 0, 0);
        PlayerStats.Health--;
    }

    private void CalculatePlayerMovement()
    {
        xInputPrev = xInput;
        // Here we're reading the horizontal input from the InputManager
        // The settings for this are defined in the Project Settings -> Input
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");


        if ((xInputPrev > 0 && xInput < 0) || (xInputPrev < 0 && xInput > 0))
        {
            changedDirection = true;
        }



        debugText.text = $"xInput: {xInput}";

        // this will be the direction we move in relative to the input
        Vector3 direction = new Vector3(xInput, 0f, zInput);

        // We then transform this based on the current direction of the 
        // camera, so we move in that direction!
        moveDirection = thirdPersonCam.TransformDirection(direction);

        if (changedDirection)
        {
            Vector3 xDirection = new Vector3(xInput, 0, 0);
            xMoveDirection = thirdPersonCam.TransformDirection(xDirection);
        }
    }

    // Within here, apply any movement to the player that has been calculated.
    private void MovePlayer()
    {
        // basically mario jumping (small jump when small tap, big jump on long press)
        // 
        if (ballRigidbody.velocity.y < 0)
        {
            // The Physics.Gravity.y part is negative. hence how we're applying downward motion here.
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (ballRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            ballRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // if we're grounded apply a speed force in the direction we're pointing with the inputs checked (keyboard)
        if (IsGrounded())
        {

            // braking - apply the equal opposite force multiplied by the breakMultiplier to the ball
            if (shiftPressed)
            {
                ballRigidbody.AddForce(new Vector3(0 - ballRigidbody.velocity.x * brakeMultiplier, 0, 0 - ballRigidbody.velocity.z * brakeMultiplier) * speed * Time.deltaTime);
            }

            // jump when the space button is pressed
            if (spacePressed)
            {
                ballRigidbody.velocity = new Vector3(ballRigidbody.velocity.x, 0, ballRigidbody.velocity.z);
                ballRigidbody.AddForce(Vector3.up * upJumpForce, ForceMode.Impulse);
                spacePressed = false;
            }

            //This reduces drag when the player adds input, and makes it stop faster. 
            // However the problem is that because this is only really active on movement, it causes issues when jumping.
            // We need to deactivate the drag when doing a jump, and reactivate after. maybe only apply when grounded?
            ballRigidbody.drag = Mathf.Lerp(maxDrag, 0, ballRigidbody.velocity.magnitude);

            // this reduces the amount of force that acts on the object if it is already 
            // moving at speed. 
            float forceMultiplier = Mathf.Clamp01((speed - ballRigidbody.velocity.magnitude) / speed);
            // now we actually perform the push 
            ballRigidbody.AddForce(moveDirection * (forceMultiplier * Time.deltaTime * forceConstant));
            if (changedDirection)
            {
                if (numTimesXChanged < 5)
                {
                    ballRigidbody.AddForce(xMoveDirection * (forceMultiplier * Time.deltaTime * forceConstant));
                    numTimesXChanged++;
                }
                else
                {
                    numTimesXChanged = 0;
                    changedDirection = false;
                }

            }
        }
        else
        {
            // Instead of limiting speed, set a small amount of air drag?
            ballRigidbody.drag = 1;
            // if we're not grounded we only want a little air control, not full air control
            ballRigidbody.AddForce(moveDirection * speed / 2 * Time.deltaTime);
        }
    }

    // a really neat raycast downwards that allows for very quick checks
    // of whether you're grounded or not
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }
}
