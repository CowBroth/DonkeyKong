using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;
    ControlActions controls;
    public Vector2 origin;
    public float vel;
    void Awake()
    {
        controls = new ControlActions();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb.linearVelocity.x >= maxSpeed)
        {
            rb.linearVelocityX = maxSpeed;
        }
        if (controls.Tools.ResetPos.triggered)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = origin;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        vel = rb.linearVelocity.x;
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
