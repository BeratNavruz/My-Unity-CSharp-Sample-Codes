using System.Collections;
using UnityEngine;
using UnityEngine.AI;

enum AnimControlEnum { Shot, Idle, Run }

public class VolleyballPlayer : MonoBehaviour
{
    public float RotY;
    public bool IsShot = false;
    bool _isCollisionBall = false;
    public Transform Ball;
    public Transform DefaultPos;
    AnimControlEnum _animControlEnum = AnimControlEnum.Idle;
    NavMeshAgent _agent;
    [SerializeField] Animator _animator;
    [SerializeField] Ball _ball;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!_isCollisionBall)
        {
            if (IsShot)
            {
                if (_ball.BallIsPlay)
                {
                    _agent.SetDestination(Ball.position);
                    if (_animControlEnum != AnimControlEnum.Run)
                    {
                        _animControlEnum = AnimControlEnum.Run;
                        AnimControl("Run");
                    }
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, DefaultPos.position) > .25f)
                {
                    _agent.SetDestination(DefaultPos.position);
                    if (_animControlEnum != AnimControlEnum.Run)
                    {
                        _animControlEnum = AnimControlEnum.Run;
                        AnimControl("Run");
                    }
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, RotY, transform.rotation.eulerAngles.z);

                    if (_animControlEnum != AnimControlEnum.Idle)
                    {
                        _animControlEnum = AnimControlEnum.Idle;
                        AnimControl("Idle");
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _isCollisionBall = true;
            if (_animControlEnum != AnimControlEnum.Shot)
            {
                _animControlEnum = AnimControlEnum.Shot;
                AnimControl("Shot");
                StartCoroutine(IECollisionBall());
            }
        }
    }

    IEnumerator IECollisionBall()
    {
        yield return new WaitForSeconds(.5f);
        _isCollisionBall = false;
    }

    public void BallOnTrigger()
    {
        IsShot = true;
    }

    public void BallOffTrigger()
    {
        IsShot = false;
    }

    public void AnimControl(string animName)
    {
        _animator.CrossFade(animName, .1f);
    }
}
