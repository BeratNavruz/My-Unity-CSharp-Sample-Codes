using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] AudioSource _ballHittingSound;

    Transform _targetField;
    [SerializeField] Transform _playerField;
    [SerializeField] Transform _opponentField;

    bool _isCollisionPlayer;
    bool _isCollisionOpponent;

    [SerializeField] Transform[] _playerTeam;
    [SerializeField] Transform[] _opponentTeam;

    public float Speed = 4;
    public float BackSpeed = 5;

    public bool BallIsPlay = true;

    [SerializeField] ScoreBoardControl _scoreBoardControl;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    IEnumerator CheckForOutOfBounds(float wait)
    {
        yield return new WaitForSeconds(wait);
        _rb.velocity = Vector3.zero;
        transform.position = _targetField.position;
        BallIsPlay = true;

        if (!StageManager8.Instance.IsGameStop)
        {
            for (int i = 0; i < _playerTeam.Length; i++)
            {
                if (_playerTeam[i].gameObject.TryGetComponent<VolleyballPlayer>(out var volleyballPlayer))
                {
                    volleyballPlayer.BallOffTrigger();
                }
            }

            for (int i = 0; i < _opponentTeam.Length; i++)
            {
                if (_opponentTeam[i].gameObject.TryGetComponent<VolleyballPlayer>(out var volleyballPlayer))
                {
                    volleyballPlayer.BallOffTrigger();
                }
            }
            StageManager8.Instance.IsGameStop = true;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _ballHittingSound.Play();
        if (other.gameObject.CompareTag("Player"))
        {
            ShotTheBall(_opponentTeam, Speed);
            _isCollisionPlayer = true;
            _isCollisionOpponent = false;
        }

        if (other.gameObject.CompareTag("BackPlayer"))
        {
            ShotTheBall(_opponentTeam, BackSpeed);
            _isCollisionPlayer = true;
            _isCollisionOpponent = false;
        }

        if (other.gameObject.CompareTag("Opponent"))
        {
            ShotTheBall(_playerTeam, Speed);
            _isCollisionOpponent = true;
            _isCollisionPlayer = false;
        }

        if (other.gameObject.CompareTag("BackOpponent"))
        {
            ShotTheBall(_playerTeam, BackSpeed);
            _isCollisionOpponent = true;
            _isCollisionPlayer = false;
        }

        if (other.gameObject.CompareTag("PlayerFloor"))
        {
            if (BallIsPlay)
            {
                BallIsPlay = false;
                _targetField = _opponentField;
                _scoreBoardControl.UpdateScoreBoard("OpponentTeam");
                if (!StageManager8.Instance.IsTeamLosePoints)
                {
                    StageManager8.Instance.IsTeamLosePoints = true;
                }
                StartCoroutine(CheckForOutOfBounds(1));
                _isCollisionPlayer = false;
                _isCollisionOpponent = false;
            }
        }

        if (other.gameObject.CompareTag("OpponentFloor"))
        {
            if (BallIsPlay)
            {
                BallIsPlay = false;
                _targetField = _playerField;
                _scoreBoardControl.UpdateScoreBoard("PlayerTeam");
                StartCoroutine(CheckForOutOfBounds(1));
                _isCollisionPlayer = false;
                _isCollisionOpponent = false;
            }
        }

        if (other.gameObject.CompareTag("ExitFloor"))
        {
            if (BallIsPlay)
            {
                BallIsPlay = false;
                if (_isCollisionPlayer)
                {
                    _targetField = _opponentField;
                    _scoreBoardControl.UpdateScoreBoard("OpponentTeam");
                    if (!StageManager8.Instance.IsTeamLosePoints)
                    {
                        StageManager8.Instance.IsTeamLosePoints = true;
                    }
                    StartCoroutine(CheckForOutOfBounds(1));
                }
                else if (_isCollisionOpponent)
                {
                    _targetField = _playerField;
                    _scoreBoardControl.UpdateScoreBoard("PlayerTeam");
                    StartCoroutine(CheckForOutOfBounds(1));
                }

                _isCollisionPlayer = false;
                _isCollisionOpponent = false;

                StartCoroutine(CheckForOutOfBounds(1));
            }
        }
    }

    void ShotTheBall(Transform[] t, float speed)
    {
        Vector3 direction = (t[Random.Range(0, t.Length)].position - transform.position).normalized;
        direction = new Vector3(direction.x, 2, direction.z);

        _rb.velocity = Vector3.zero;
        _rb.AddForce(direction * speed, ForceMode.Impulse);
    }
}
