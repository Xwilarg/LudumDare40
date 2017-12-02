using UnityEngine;

public class MoveByBullets : MonoBehaviour {

    private void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
