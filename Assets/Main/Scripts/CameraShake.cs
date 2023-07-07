using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    CinemachineVirtualCamera VirtualCamera;

    float shakerTimer;
    float shakerTimerTotal;
    float startingIntensity;
    private void Awake()
    {
        Instance = this;
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakerTimer = time;
        shakerTimerTotal = time;
    }
    private void Update()
    {
        if (shakerTimer >0)
        {
            shakerTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - shakerTimer / shakerTimerTotal);
        }
    }
}
