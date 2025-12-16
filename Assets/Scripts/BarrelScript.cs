using System.Threading;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;
    public float minSpeed;
    public float vel;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb.linearVelocity.magnitude >= maxSpeed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    
        vel = rb.linearVelocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            rb.AddForce(-collision.transform.right * (minSpeed * 5), ForceMode2D.Impulse);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
