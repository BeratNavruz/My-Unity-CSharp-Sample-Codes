using UnityEngine;

public class CanvasRotate : MonoBehaviour
{
    public Camera TargetCamera;
    Vector3 _direction;

    void LateUpdate()
    {
        _direction = TargetCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-_direction);
    }
}
