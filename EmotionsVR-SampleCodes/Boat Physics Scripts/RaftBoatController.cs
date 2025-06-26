using UnityEngine;

enum ShovelAnimR { Idle, Forward, Back }
enum ShovelAnimL { Idle, Forward, Back }

public class RaftBoatController : MonoBehaviour
{
    public bool BoatMovementIsEnabled = true;

    #region Animations
    public Animator AnimatorR;
    public Animator AnimatorL;

    ShovelAnimR _shovelAnimR = ShovelAnimR.Idle;
    ShovelAnimL _shovelAnimL = ShovelAnimL.Idle;
    #endregion

    public Rigidbody raftRigidbody;
    public float forwardForce = 10f;
    public float turnForce = 5f;
    public float rotationSpeed = 5f;

    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;

    Vector2 rightStickInput;
    Vector2 leftStickInput;
    Vector3 movementForce;
    float rotation;
    Quaternion deltaRotation;

    void FixedUpdate()
    {
        if (BoatMovementIsEnabled)
        {
            rightStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController);

            leftStickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);

            movementForce = Vector3.zero;
            rotation = 0f;

            if (rightStickInput.y > 0.1f && leftStickInput.y > 0.1f)
            {
                movementForce += -transform.up * forwardForce * 2f;
                Shovel_R_AnimControl(ShovelAnimR.Forward, "Shovel_R_Forward_Anim");
                Shovel_L_AnimControl(ShovelAnimL.Forward, "Shovel_L_Forward_Anim");
            }

            else if (rightStickInput.y < -0.1f && leftStickInput.y < -0.1f)
            {
                movementForce += transform.up * forwardForce * 2f;
                Shovel_R_AnimControl(ShovelAnimR.Back, "Shovel_R_Back_Anim");
                Shovel_L_AnimControl(ShovelAnimL.Back, "Shovel_L_Back_Anim");
            }

            else if (rightStickInput.y > 0.1f)
            {
                movementForce += -transform.up * forwardForce + -transform.right * turnForce;
                rotation -= rotationSpeed;
                Shovel_R_AnimControl(ShovelAnimR.Forward, "Shovel_R_Forward_Anim");
            }

            else if (leftStickInput.y > 0.1f)
            {
                movementForce += -transform.up * forwardForce + transform.right * turnForce;
                rotation += rotationSpeed;
                Shovel_L_AnimControl(ShovelAnimL.Forward, "Shovel_L_Forward_Anim");
            }

            else if (rightStickInput.y < -0.1f)
            {
                movementForce += transform.up * forwardForce + transform.right * turnForce;
                rotation += rotationSpeed;
                Shovel_R_AnimControl(ShovelAnimR.Back, "Shovel_R_Back_Anim");
            }

            else if (leftStickInput.y < -0.1f)
            {
                movementForce += transform.up * forwardForce + -transform.right * turnForce;
                rotation -= rotationSpeed;
                Shovel_L_AnimControl(ShovelAnimL.Back, "Shovel_L_Back_Anim");
            }

            else
            {
                Shovel_R_AnimControl(ShovelAnimR.Idle, "Shovel_R_Idle_Anim");
                Shovel_L_AnimControl(ShovelAnimL.Idle, "Shovel_L_Idle_Anim");
            }

            raftRigidbody.AddForce(movementForce, ForceMode.Force);

            if (rotation != 0f)
            {
                deltaRotation = Quaternion.Euler(0f, 0f, rotation * Time.fixedDeltaTime);
                raftRigidbody.MoveRotation(raftRigidbody.rotation * deltaRotation);
            }
        }
        else
        {
            Shovel_R_AnimControl(ShovelAnimR.Idle, "Shovel_R_Idle_Anim");
            Shovel_L_AnimControl(ShovelAnimL.Idle, "Shovel_L_Idle_Anim");
        }
    }

    void Shovel_R_AnimControl(ShovelAnimR shovelAnimR, string animName)
    {
        if (_shovelAnimR != shovelAnimR)
        {
            _shovelAnimR = shovelAnimR;
            AnimatorR.CrossFade(animName, .1f);
        }
    }

    void Shovel_L_AnimControl(ShovelAnimL shovelAnimL, string animName)
    {
        if (_shovelAnimL != shovelAnimL)
        {
            _shovelAnimL = shovelAnimL;
            AnimatorL.CrossFade(animName, .1f);
        }
    }
}
