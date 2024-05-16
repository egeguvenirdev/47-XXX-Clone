using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class CamManager : MonoBehaviour
{
    [Header("Camera Components")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera ortograficCam;

    [Header("Cam Follow Settings")]
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    [SerializeField] private float clampLocalX = 1.5f;

    [Header("Shake Settings")]
    [SerializeField] private MMCameraShaker camShaker;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeAmplitude;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float shakeAmplitudeX;
    [SerializeField] private float shakeAmplitudeY;
    [SerializeField] private float shakeAmplitudeZ;
    [SerializeField] private bool unscaledTime;

    private float camBasicSize = 0.5f;

    public Camera MainCam { get => mainCam; set => mainCam = value; }

    public void Init()
    {
        ActionManager.CamShake += OnCamShake;
        ActionManager.SetCamSize += OnSetCamSize;
        ActionManager.GetOrtograficScreenToWorldPoint += OnGetOrtograficCam;

    }

    public void DeInit()
    {
        ActionManager.CamShake -= OnCamShake;
        ActionManager.SetCamSize -= OnSetCamSize;
        ActionManager.GetOrtograficScreenToWorldPoint -= OnGetOrtograficCam;

        camBasicSize = MainCam.orthographicSize;
    }

    private void OnSetCamSize(float sizeMultiplier, float anchorPos, float ratio)
    {
        Debug.Log(ratio);
        MainCam.orthographicSize = camBasicSize * sizeMultiplier * ratio;
        MainCam.transform.localPosition = new Vector3(anchorPos + 0.1f, 10, 2.1f - ratio);
    }

    private Vector3 OnGetOrtograficCam(Vector3 targetPos)
    {
        return ortograficCam.ScreenToWorldPoint(targetPos);
    }

    private void OnCamShake()
    {
        camShaker.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency, shakeAmplitudeX, shakeAmplitudeY, shakeAmplitudeZ, unscaledTime);
    }
}