using UnityEngine;

public class DeleteCollision : MonoBehaviour {

    private const float refTime = 3f;
    private float currTime;
    public GameObject owner { set; get; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((collision.collider.CompareTag("Wall") || collision.collider.CompareTag("BulletFreeze")) && collision.gameObject != owner)
            || collision.collider.CompareTag("ItemBase1") || collision.collider.CompareTag("ItemSup"))
        {
            if (tag == "Bullet2" && collision.collider.CompareTag("BulletFreeze"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (!collision.collider.CompareTag("BulletFreeze"))
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        currTime = 0f;
    }

    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= refTime)
            Destroy(gameObject);
    }
}
