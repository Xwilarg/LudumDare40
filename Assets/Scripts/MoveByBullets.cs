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
            Vector2 velo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (Mathf.Abs(velo.x) < Mathf.Abs(velo.y))
            {
                if (velo.x > 0)
                    rb.velocity = new Vector2(10f, 0.0f);
                else
                    rb.velocity = new Vector2(-10f, 0.0f);
            }
            else
            {
                if (velo.y > 0)
                    rb.velocity = new Vector2(0.0f, 10f);
                else
                    rb.velocity = new Vector2(0.0f, -10f);
            }
            Destroy(collision.gameObject);
        }
    }
}
