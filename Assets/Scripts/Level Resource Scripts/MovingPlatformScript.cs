using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] Collider platformCollider;
    [SerializeField] Vector3 destinationPos;

    PlayerPresentColliderScript playerPresentScript;
    bool playerOnPlatform = false;
    Vector3 initialPos, currentTargetPos;
    Rigidbody platformRb;
    float movementSpeed = 5f;
    float xLimit = 0.5f, yLimit = 0.5f, zLimit = 0.5f;

    private void Start()
    {
        playerPresentScript = platformCollider.GetComponent<PlayerPresentColliderScript>();
        initialPos = transform.localPosition;
        platformRb = GetComponent<Rigidbody>();
        currentTargetPos = destinationPos;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (currentTargetPos - transform.localPosition).normalized;

        platformRb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);

        if (shouldChangeDirecton())
        {
            if (currentTargetPos == initialPos)
            {
                currentTargetPos = destinationPos;
            }
            else
            {
                currentTargetPos = initialPos;
            }
        }

    }

    private bool shouldChangeDirecton()
    {
        float diffX = transform.localPosition.x - currentTargetPos.x;
        if (diffX > xLimit)
            return false;
        float diffY = transform.localPosition.y - currentTargetPos.y;
        if (diffY > yLimit)
            return false;
        float diffZ = transform.localPosition.z - currentTargetPos.z;
        if (diffZ > zLimit)
            return false;

        return true;
    }
}
