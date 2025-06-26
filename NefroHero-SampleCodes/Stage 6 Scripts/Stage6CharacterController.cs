using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6CharacterController : InstantiateAvatar
{
    Animator _characterAnims;

    [SerializeField] Transform center_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Transform right_pos;

    Rigidbody rb;
    Vector3 dir;

    int current_pos = 0;
    float _gravity;
    [SerializeField] float side_speed;
    public float running_speed;
    [SerializeField] float jump_Force;
    public bool isRun;
    [SerializeField] bool isJump;

    #region Parmak Hareket Kontrolü
    Vector2 startTouchPos;
    Vector2 currentTouchPos;
    Vector2 distance;
    //Vector2 distance_end;
    Touch touch;
    bool stopTouch = false;
    [SerializeField] float swipeRange = 50f;
    //[SerializeField] float tapRange = 10f;
    #endregion

    public Stage6GameManager stage6GameManager;
    public Stage6MapSwapper stage6MapSwapper;

    protected override void Start()
    {
        base.Start();
        _gravity = -9.81f;
        Physics.gravity = new Vector3(0, _gravity * 2, 0);
        _characterAnims = transform.GetChild(0).gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        current_pos = 0;
    }

    void Update()
    {
        if (isRun)
        {
            transform.position = new Vector3(transform.position.x + (running_speed * Time.deltaTime), transform.position.y, transform.position.z);
            SwipeCheck();
        }

        if (current_pos == 0) X_PosSwap(center_pos);

        else if (current_pos == 1) X_PosSwap(left_pos);

        else if (current_pos == 2) X_PosSwap(right_pos);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            if (isRun) CharacterAnimChange("Running");
            else CharacterAnimChange("Idle");
        }

        if (other.gameObject.name == "Train")
        {
            StartCoroutine(stage6GameManager.ReLoadScene());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Swapper Trigger")
        {
            stage6MapSwapper.Swapper();
        }

        if (other.gameObject.name == "Destroyed Trigger")
        {
            stage6MapSwapper.Destroyed();
        }
    }

    private void SwipeCheck()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
                stopTouch = false;
            }
            else if (touch.phase == TouchPhase.Moved && !stopTouch)
            {
                currentTouchPos = touch.position;
                distance = currentTouchPos - startTouchPos;

                if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                {
                    // Sağa veya sola kaydırma
                    if (distance.x > swipeRange)
                    {
                        SidePos("Right");
                        stopTouch = true;
                    }
                    else if (distance.x < -swipeRange)
                    {
                        SidePos("Left");
                        stopTouch = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                stopTouch = true;
            }
        }
    }

    void X_PosSwap(Transform pos)
    {
        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, pos.position.z)) >= .1f)
        {
            dir = new Vector3(transform.position.x, transform.position.y, pos.position.z) - transform.position;
            transform.Translate(side_speed * Time.deltaTime * dir.normalized, Space.World);
        }
    }

    public void Jump()
    {
        if (!isJump && isRun)
        {
            isJump = true;
            rb.velocity = Vector3.up * jump_Force;
            CharacterAnimChange("Jump");
        }
    }

    public void SidePos(string side)
    {
        if (isRun)
        {
            if (current_pos == 0)
            {
                if (side == "Left")
                    current_pos = 1;
                else if (side == "Right")
                    current_pos = 2;
            }
            else if (current_pos == 1)
            {
                if (side == "Right")
                    current_pos = 0;
            }
            else if (current_pos == 2)
            {
                if (side == "Left")
                    current_pos = 0;
            }
        }
    }

    public void ButtonStart(GameObject button)
    {
        button.SetActive(false);
        isRun = true;
        CharacterAnimChange("Running");
    }

    public void CharacterAnimChange(string animName)
    {
        _characterAnims.CrossFade(animName, .1f);
    }
}
