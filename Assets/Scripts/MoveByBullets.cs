using UnityEngine;

public class MoveByBullets : MonoBehaviour {

    private Rigidbody2D rb;
    private bool alreadyMoved;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        alreadyMoved = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alreadyMoved) return;
        if (collision.collider.CompareTag("Bullet"))
        {
            alreadyMoved = true;
            rb.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            Destroy(collision.gameObject);
        }
    }
}
