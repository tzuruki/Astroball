using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineFreeLook freelook;

    float ySpeed = 0.5f;
    float xSpeed = 5;

    void Start()
    {
        freelook = GetComponent<CinemachineFreeLook>();
    }

    // LateUpdate is called AFTER the Update() method - apparently important for cameras as things can happen in update
    // and thats not where you do camera stuff!
    void LateUpdate()
    {
        freelook.m_XAxis.m_MaxSpeed = xSpeed * PlayerStats.MouseSensitivity;
        freelook.m_YAxis.m_MaxSpeed = ySpeed * PlayerStats.MouseSensitivity;
    }
}
