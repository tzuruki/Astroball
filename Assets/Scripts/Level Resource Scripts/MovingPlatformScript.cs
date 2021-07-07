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
    float xLimit = 1f, yLimit = 1f, zLimit = 1f;

    private void Start()
    {
        initialPos = transform.localPosition;
        platformRb = GetComponent<Rigidbody>();
        currentTargetPos = destinationPos;
    }

    private void FixedUpdate()
    {
        Debug.Log(initialPos);
        Debug.Log(transform.localPosition);

        if (shouldChangeDirecton())
        {
            Debug.Log("shouldChange");
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
            Debug.Log($"CurrentTargetPos: {currentTargetPos}");

            Vector3 direction = (currentTargetPos - transform.localPosition).normalized;

            Debug.Log(direction);

            platformRb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
        }
    }

    private bool shouldChangeDirecton()
    {
        float diffX = transform.position.x - System.Math.Abs(currentTargetPos.x);
        if (diffX > xLimit)
            return false;
        float diffY = transform.position.y - System.Math.Abs(currentTargetPos.y);
        if (diffY > yLimit)
            return false;
        float diffZ = transform.position.z - System.Math.Abs(currentTargetPos.z);
        if (diffZ > zLimit)
            return false;

        return true;
    }
}
