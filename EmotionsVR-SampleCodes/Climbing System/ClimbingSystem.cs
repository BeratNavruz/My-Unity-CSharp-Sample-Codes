using UnityEngine;

public class ClimbingSystem : MonoBehaviour
{
    public CharacterController characterController;
    public OVRInput.Button gripButton;
    public bool OnButton;
    private Vector3 previousHandPosition;
    public bool isClimbing = false;

    [SerializeField] private ClimbingSystem _climbingSystem;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = false;
            if (OnButton)
            {
                OnButton = false;
                OVRPlayerClimbTriggerController.Instance.GravityControl();
            }
        }
    }

    void Update()
    {
        if (isClimbing)
        {
            if (OVRInput.GetDown(gripButton))
            {
                _climbingSystem.OnButton = false;
                OnButton = true;
                OVRPlayerClimbTriggerController.Instance.GravityControl();
                previousHandPosition = transform.position;
            }

            if (OVRInput.GetUp(gripButton))
            {
                OnButton = false;
                OVRPlayerClimbTriggerController.Instance.GravityControl();
            }

            if (OnButton)
            {
                Vector3 handDelta = transform.position - previousHandPosition;

                characterController.Move(-handDelta);

                previousHandPosition = transform.position;
            }
        }
    }
}
