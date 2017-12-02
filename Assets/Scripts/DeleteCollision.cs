using UnityEngine;

public class DeleteCollision : MonoBehaviour {

    private const float refTime = 3f;
    private float currTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
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
