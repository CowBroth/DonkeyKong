using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;
    public float minSpeed;
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
        origin = transform.position;
    }
    void Update()
    {
        if (rb.linearVelocity.magnitude >= maxSpeed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
        if (controls.Tools.ResetPos.triggered)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = origin;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        vel = rb.linearVelocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            rb.AddForce(-collision.transform.right * (minSpeed * 5), ForceMode2D.Impulse);
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
