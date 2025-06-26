using UnityEngine;
using Cinemachine;

public class DynamicFOV_Camera : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float referenceAspect;
    public float referenceFOV;

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        AdjustFOV();
    }

    void AdjustFOV()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float aspectRatioMultiplier = referenceAspect / currentAspect;
        cinemachineVirtualCamera.m_Lens.FieldOfView = referenceFOV * aspectRatioMultiplier;
    }
}
