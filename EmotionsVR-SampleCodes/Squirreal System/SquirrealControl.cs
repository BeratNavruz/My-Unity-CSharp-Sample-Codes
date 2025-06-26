using UnityEngine;

public enum AnimatorControl { Run, Idle }

public class SquirrealControl : MonoBehaviour
{
    private Animator _animator;
    private AnimatorControl _animatorControl = AnimatorControl.Idle;

    public Transform _playerTarget;
    public Transform _finishTarget;
    private Transform _currentTarget;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private bool _isStart = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isStart && _currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _currentTarget.position);

            if (distanceToTarget > _stopDistance)
            {
                if (_animatorControl != AnimatorControl.Run)
                {
                    _animator.CrossFade("Run", .1f);
                    _animatorControl = AnimatorControl.Run;
                }

                Vector3 direction = (_currentTarget.position - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;

                Vector3 targetDirection = (_currentTarget.position - transform.position).normalized;
                targetDirection.y = 0;
                if (targetDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
                }
            }
            else if (_currentTarget == _playerTarget)
            {
                if (_animatorControl != AnimatorControl.Idle)
                {
                    StartControl(false);
                    _animator.CrossFade("Idle", .1f);
                    _animatorControl = AnimatorControl.Idle;
                    StartCoroutine(SquirrealSystem.Instance.IEWait());
                }
            }
        }
        else
        {
            if (_animatorControl != AnimatorControl.Idle)
            {
                _animator.CrossFade("Idle", .1f);
                _animatorControl = AnimatorControl.Idle;
            }
        }
    }

    public void StartControl(bool isStart)
    {
        _isStart = isStart;
    }

    public void TargetSwapper(Transform target)
    {
        _currentTarget = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _finishTarget.gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
