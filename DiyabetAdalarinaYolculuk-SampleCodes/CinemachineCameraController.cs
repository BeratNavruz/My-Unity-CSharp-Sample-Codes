using UnityEngine;
using Cinemachine;

public class CinemachineCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SwapCameraPositionFunc(Transform T_LookAt, Transform T_Follow)
    {
        cinemachineVirtualCamera.LookAt = T_LookAt;
        cinemachineVirtualCamera.Follow = T_Follow;
    }
}
