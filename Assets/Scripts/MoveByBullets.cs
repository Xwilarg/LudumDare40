using UnityEngine;

public class MoveByBullets : MonoBehaviour {

    private Rigidbody2D rb;
    public Vector2 direction;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            rb.AddForce(direction * 1000f, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
    }
}
