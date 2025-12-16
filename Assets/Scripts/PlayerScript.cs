using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int lives = 3;
    public float speed;
    public float jumpHeight;
    public float climbSpeed;
    public float inputAxis;
    [HideInInspector] public float jumpInput;
    public Vector2 origin;
    public bool grounded;
    public ControlActions controls;
    public bool isMoving;
    public bool isClimbing;
    

    public IdleState idleState;
    public JumpState jumpState;
    public ClimbingState climbState;
    public StateMachine sm;
    //public CharacterController cc;
    Rigidbody2D rb;
    public SpriteRenderer sprite;
    CapsuleCollider2D col;
    public ManagerScript manager;

    public Vector3 rayPos = new Vector2(-0.25f, 0);
    public float rayLength;
    LayerMask terrain;
    LayerMask mask;
    public Animator anim;
    public Vector2 touchPosition;
    private void Awake()
    {
        controls = new ControlActions();

        mask = LayerMask.GetMask("Terrain");
        print("mask value=" + mask.value);
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sm = gameObject.AddComponent<StateMachine>();
        idleState = new IdleState(this, sm);
        jumpState = new JumpState(this, sm);
        climbState = new ClimbingState(this, sm);
        sm.Init(idleState);
        //cc = GetComponent<CharacterController>();
        terrain = LayerMask.NameToLayer("Terrain");
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        origin = transform.position;
    }
    public void Update()
    {
        inputAxis = controls.Movement.WalkAxis.ReadValue<float>();
        jumpInput = controls.Movement.Jump.ReadValue<float>();
        anim.SetBool("Walk", isMoving);
        anim.SetBool("MidAir", grounded);
        touchPosition = controls.Movement.TouchAxis.ReadValue<Vector2>();

         if (controls.Movement.TouchBool.IsPressed())
         {
            if (touchPosition.x < Screen.width / 2f)
            {
                inputAxis = -1f;
            }
            else
            {
                inputAxis = 1f;
            }
         }

        if (controls.Tools.ResetPos.triggered)
        {
            transform.position = origin;
        }
        }
        /*if (!controls.Movement.TouchBool.IsPressed())
        {
            inputAxis = 0;
            return;
        }*/
        
    
    private void FixedUpdate()
    {
        RaycastHit2D ray1 = Physics2D.Raycast(new Vector3(transform.position.x - rayPos.x, transform.position.y + rayPos.y), -Vector2.up, rayLength, mask);
        RaycastHit2D ray2 = Physics2D.Raycast(new Vector3(transform.position.x + rayPos.x, transform.position.y + rayPos.y), -Vector2.up, rayLength, mask);

        grounded = false;
        if( ray1 || ray2 )
        {
            grounded = true;
        }
        
        Debug.DrawRay(new Vector3(transform.position.x - rayPos.x, transform.position.y + rayPos.y), -transform.up * rayLength);
        Debug.DrawRay(new Vector3(transform.position.x + rayPos.x, transform.position.y + rayPos.y),  -transform.up * rayLength);
        sm.ChangeState((!grounded) ? jumpState : idleState);
        if (isClimbing)
        {
            sm.ChangeState(climbState);
        }
        sm.CurrentState.PhysicsUpdate();
    }

    public void PlayerMove()
    {
        rb.linearVelocity = new Vector2(inputAxis * speed, -1);   
    }

    public void PlayerJump()
    {
        rb.linearVelocity = new Vector2(inputAxis * 2, jumpHeight);
    }

    public void Climbing()
    {
        rb.linearVelocity = new Vector2(0, climbSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BottomLadder"))
        {
            print("ladder");
            isClimbing = true;
            col.excludeLayers = mask;
            transform.position = new Vector2(collision.transform.position.x, transform.position.y);
            anim.SetBool("Climb", true);
        }
        if (collision.gameObject.CompareTag("TopLadder"))
        {
            print("stop ladder");
            isClimbing = false;
            col.excludeLayers = 0;
            anim.SetBool("Climb", false);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Barrel"))
        {
            transform.position = origin;
            if (lives == 3)
            {
                lives--;
                manager.DestroyLife1();
            }
            if (lives == 2)
            {
                lives--;
                manager.DestroyLife2();
            }
            if (lives == 1)
            {
                lives--;
                manager.DestroyLife3();
            }
            if (lives == 0)
            {
                manager.DestroyLife4();
            }
        }
    }
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
}
