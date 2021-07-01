using UnityEngine;
using Cinemachine;

//Entirely copied from https://forum.unity.com/threads/cm-2-3-4-recenter-to-target-heading-stuck-after-first-cycle.691306/#post-4631089

[RequireComponent(typeof(CinemachineFreeLook)), DisallowMultipleComponent]
public class CinemachineCameraScript : MonoBehaviour
{
    public CinemachineInputAxisDriver xAxis;
    public CinemachineInputAxisDriver yAxis;

    private CinemachineFreeLook freeLook;

    private void Awake()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
        freeLook.m_XAxis.m_MaxSpeed = freeLook.m_XAxis.m_AccelTime = freeLook.m_XAxis.m_DecelTime = 0;
        freeLook.m_XAxis.m_InputAxisName = string.Empty;
        freeLook.m_YAxis.m_MaxSpeed = freeLook.m_YAxis.m_AccelTime = freeLook.m_YAxis.m_DecelTime = 0;
        freeLook.m_YAxis.m_InputAxisName = string.Empty;
    }

    private void OnValidate()
    {
        xAxis.Validate();
        yAxis.Validate();
    }

    private void Reset()
    {
        xAxis = new CinemachineInputAxisDriver
        {
            multiplier = -100f,
            accelTime = 0.1f,
            decelTime = 0.1f,
            name = "Mouse X",
        };
        yAxis = new CinemachineInputAxisDriver
        {
            multiplier = 0.01f,
            accelTime = 0.1f,
            decelTime = 0.1f,
            name = "Mouse Y",
        };
    }

    private void LateUpdate()
    {
        bool changed = yAxis.Update(Time.deltaTime, ref freeLook.m_YAxis);
        changed |= xAxis.Update(Time.deltaTime, ref freeLook.m_XAxis);
        if (changed)
        {
            freeLook.m_RecenterToTargetHeading.CancelRecentering();
            freeLook.m_YAxisRecentering.CancelRecentering();
        }
    }
}
