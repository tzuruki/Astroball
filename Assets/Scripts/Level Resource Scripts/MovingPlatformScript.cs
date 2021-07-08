using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] Collider platformCollider;
    [SerializeField] Vector3 destinationPos;

    Vector3 initialPos, currentTargetPos;
    Rigidbody platformRb;
    float movementSpeed = 5f;
    float limit = 2.0f;

    private void Start()
    {
        initialPos = transform.localPosition;
        platformRb = GetComponent<Rigidbody>();
        currentTargetPos = destinationPos;
    }

    private void FixedUpdate()
    {
       
        if (shouldChangeDirecton())
        {
           
            if (currentTargetPos == initialPos)
            {
                currentTargetPos = destinationPos;

            }
            else if (currentTargetPos == destinationPos)
            {
                currentTargetPos = initialPos;
            }
        }
        else
        {
            Vector3 direction = (currentTargetPos - transform.localPosition).normalized;

            platformRb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);

        }
    }

    private bool shouldChangeDirecton()
    {
        var diff = Vector3.Distance(transform.localPosition, currentTargetPos);

        if (diff > limit)
        {
            return false;
        }

        return true;
    }
}
