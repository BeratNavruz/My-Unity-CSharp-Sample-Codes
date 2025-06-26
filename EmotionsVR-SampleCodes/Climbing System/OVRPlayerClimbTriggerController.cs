using UnityEngine;

enum HasGravity { GravityActive, GravityPassive }

public class OVRPlayerClimbTriggerController : MonoBehaviour
{
    public static OVRPlayerClimbTriggerController Instance { get; private set; }
    public ClimbingSystem[] climbingSystems;
    float _gravity;
    HasGravity _hasGravity = HasGravity.GravityActive;
    OVRPlayerController oVRPlayerController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        oVRPlayerController = GetComponent<OVRPlayerController>();
        _gravity = oVRPlayerController.GravityModifier;
    }

    public void GravityControl()
    {
        if (!climbingSystems[0].OnButton && !climbingSystems[1].OnButton)
        {
            if (_hasGravity == HasGravity.GravityPassive)
            {
                oVRPlayerController.GravityModifier = _gravity;
                _hasGravity = HasGravity.GravityActive;
            }
        }
        else
        {
            if (_hasGravity == HasGravity.GravityActive)
            {
                oVRPlayerController.GravityModifier = 0;
                _hasGravity = HasGravity.GravityPassive;
            }
        }
    }
}
