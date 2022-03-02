using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //player attribute
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f; // our max speed
    [SerializeField] private float jumpForce = 5F;
    [SerializeField] private float MaxrollSpeed = 250f;
    private float deltaRollSpeed = 1.6f;
    private float rollSpeed;
    [SerializeField] private float climbSpeed = 50f;
    InputPlayer controls;

    //player camera
    public Transform camera;

    float cameraFace;

    [SerializeField] int hp;
    float damageRate = 1f;

    //detect terrain layer
    [SerializeField] LayerMask terrain;

    bool isGrounded;
    bool isRunning;
    bool isRolling;
    bool isCrouching;
    bool climbing;
    bool isJumping;
    bool isDead;

    float timerCrouch = 1f;
    float lastTimeCrouch;

    float timerStep = 0.5f;
    float lastTimeStep;

    Vector3 moveDirection; //I commented this
    Vector3 cameraInput;

    Rigidbody rb;
    Animator animator;
    CapsuleCollider hitbox;
    AudioSource audioSrc;
    public AudioClip step;
    public float fallMultiplier = 2.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        isRolling = false;
        hp = 1;
        lastTimeCrouch = Time.time;
        lastTimeStep = Time.time;
        controls = new InputPlayer();
        isJumping = false;
        isDead = false;
        //controls.Player.Jump.performed += _ => Jump();
        // controls.Player.Run.started += context => SetRunning(true);
        // controls.Player.Run. += context => SetRunning(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            RaycastHit LHit;

            if (Physics.Raycast(transform.position + new Vector3(0f, 1f, 0f), transform.forward
                , out LHit, 0.7f, LayerMask.GetMask("climable")) && !isCrouching)
            {
                climbing = true;
                rb.useGravity = false;
                animator.SetBool("Climbing", true);
            }
            else
            {
                climbing = false;
                rb.useGravity = true;
                animator.SetBool("Climbing", false);
            }

            if (climbing)
            {
                Vector2 LeftStick = controls.Player.Move.ReadValue<Vector2>();
                moveDirection = new Vector3(0, LeftStick.y, 0).normalized;
                if (moveDirection.magnitude > 0)
                {
                    rb.velocity = moveDirection * climbSpeed * Time.deltaTime;
                }
            }

            if (!isRolling)
            {
                if (isGrounded && !climbing)
                    Move();
                CheckGrounded();
            }
            else
            {
                Rolling();
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
            }
        }
    }

    //player movement
    public void Move()
    {
        Vector2 LeftStick = controls.Player.Move.ReadValue<Vector2>();
        //input player
        float moveZ = LeftStick.y;
        float moveX = LeftStick.x;

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        //rotation relative camera
        cameraFace = Camera.main.transform.eulerAngles.y; //get angle camera

        // we rotate them around Y, assuming your inputs are in X and Z in the myInputs vector
        cameraInput = Quaternion.Euler(0, cameraFace, 0) * moveDirection;

        //move player
        if (moveDirection.magnitude > 0)
        {
            //our player run if left shift is pressed or not
            if (isRunning && !isCrouching)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }

        //player movement animations
        animator.SetFloat("Speed", rb.velocity.magnitude / runSpeed);
    }

    void Walk()
    {
        rb.velocity = cameraInput * walkSpeed;
        float angle = Mathf.Atan2(cameraInput.x, cameraInput.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        timerStep = 0.5f;
        if (Time.time - lastTimeStep > timerStep)
        {
            audioSrc.maxDistance = 500f;
            audioSrc.clip = step;
            gameObject.GetComponent<SoundScript>().MakeSoundForZombie();
            lastTimeStep = Time.time;
        }
    }
    void Run()
    {
        rb.velocity = cameraInput * runSpeed;
        float angle = Mathf.Atan2(cameraInput.x, cameraInput.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        timerStep = 0.3f;
        if (Time.time - lastTimeStep > timerStep)
        {
            audioSrc.maxDistance = 1000f;
            audioSrc.clip = step;
            gameObject.GetComponent<SoundScript>().MakeSoundForZombie();
            lastTimeStep = Time.time;
        }
    }

    public void Jump()
    {
        if (!isJumping && !isDead)
        {
            if (climbing)
            {
                animator.SetTrigger("JumpTrigger");
                Vector3 jumpAway = -transform.forward * 5;
                rb.AddForce(jumpAway + Vector3.up * jumpForce, ForceMode.Impulse);
                transform.forward *= -1;
                isJumping = true;
            }
            else if (isGrounded)
            {
                //transform.position += new Vector3(0, 0.25f, 0);
                animator.SetTrigger("JumpTrigger");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
            }
        }

    }

    public void SetJump(bool jump)
    {
        isJumping = jump;
    }

    void CheckGrounded()
    {
        RaycastHit rch;
        CapsuleCollider cpc = GetComponent<CapsuleCollider>();
        if (Physics.Raycast(transform.position + Vector3.up * .2f, -Vector3.up, out rch, .25f, terrain) && !isJumping)
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }

    public void SetRunning(bool run)
    {
        if (!isCrouching)
            isRunning = run;
    }

    public void CrouchRollHandler()
    {
        if (isRunning)
        {
            if (!isRolling)
                Roll();
        }
        else if (Time.time - lastTimeCrouch > timerCrouch)
        {
            Crouch();
        }
    }

    public void Roll()
    {
        animator.SetTrigger("RollTrigger");
        isRolling = true;
        rollSpeed = MaxrollSpeed;
    }

    public void Rolling()
    {
        rb.AddForce(rollSpeed * transform.forward * Time.deltaTime);
        rollSpeed -= rollSpeed * deltaRollSpeed * Time.deltaTime;
        if (rollSpeed < 5)
        {
            rb.AddForce(MaxrollSpeed * transform.forward * Time.deltaTime);
            rb.velocity = new Vector3(0, 0, 0);
            Vector3 RollLand = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            rb.MovePosition(RollLand);
        }
    }

    public void SetRolling(bool roll)
    {
        isRolling = roll;
    }

    public void Crouch()
    {
        if (isCrouching)
        {
            animator.SetBool("IsCrouching", false);
            isCrouching = false;
            hitbox.height *= 2;
            hitbox.radius *= 2;
            hitbox.center *= 2;
            lastTimeCrouch = Time.time;
        }
        else
        {
            animator.SetBool("IsCrouching", true);
            isCrouching = true;
            hitbox.radius /= 2;
            hitbox.height /= 2;
            hitbox.center /= 2;
            lastTimeCrouch = Time.time;
        }
    }

    public void TriggerLedgeAnim()
    {
        animator.SetTrigger("LedgeGrab");
        climbing = false;
        FreezePosition();
    }

    public void ClimbLedge()
    {
        UnfreezePosition();
        rb.MovePosition(rb.position + transform.up * 3);
        rb.MovePosition(rb.position + transform.forward * 2);
    }

    public void FreezePosition()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezePosition()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
    }
}
