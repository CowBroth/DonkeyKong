using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int lives;
    public float speed;
    public float jumpHeight;
    float inputAxis;
    [HideInInspector] public float jumpInput;
    public Vector2 origin;
    public bool grounded;
    ControlActions controls;

    public IdleState idleState;
    public JumpState jumpState;
    public StateMachine sm;
    //public CharacterController cc;
    Rigidbody2D rb;

    public Vector3 rayPos = new Vector2(-0.25f, 0);
    public float rayLength;
    LayerMask terrain;
    LayerMask mask;

    //NOTE: raycast still doesn't hit ground, check ray pos or layer

    private void Awake()
    {
        controls = new ControlActions();

        mask = LayerMask.GetMask("Terrain");
        print("mask value=" + mask.value);
    }
    private void Start()
    {
        sm = gameObject.AddComponent<StateMachine>();
        idleState = new IdleState(this, sm);
        jumpState = new JumpState(this, sm);
        sm.Init(idleState);
        //cc = GetComponent<CharacterController>();
        terrain = LayerMask.NameToLayer("Terrain");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputAxis = controls.Movement.WalkAxis.ReadValue<float>();
        jumpInput = controls.Movement.Jump.ReadValue<float>();
        if (controls.Tools.ResetPos.triggered)
        {
            transform.position = origin;
        }
    }
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
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
}
