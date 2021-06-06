using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float xInput, zInput;
    Vector3 offset;
    private float startingXRotation = 23f;
    [SerializeField] private float cameraSwayValue = 1f;
    [SerializeField] bool enableCameraSway = true;

    // Use this for initialization
    void Start()
    {
        offset = transform.position;
    }

    // LateUpdate is called AFTER the Update() method - apparently important for cameras as things can happen in update
    // and thats not where you do camera stuff!
    void LateUpdate()
    {
        // This is what handles the player dropping out of camera view
        // i.e. if we fall past a certain y coord then stop moving
        if (player.transform.position.y >= -15) {
            transform.position = player.transform.position + offset;
        }

        // toggleable camera sway - higher values make you feel motion sick its not nice.
        if (enableCameraSway)
        {
            zInput = Input.GetAxis("Horizontal");
            xInput = Input.GetAxis("Vertical");
            // This'll apply a small rotation to the x/z angles on the camera so it looks like the world is tilting a la monkey ball.
            transform.localRotation = Quaternion.Euler((startingXRotation + (xInput * cameraSwayValue)), 0, (zInput * cameraSwayValue));
        }

    }
}
