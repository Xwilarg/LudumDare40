using UnityEngine;

public class KillPlayer : MonoBehaviour {

    public GameObject player;
    private Rigidbody2D rb;
    private PlayerController pc;

    private const float speed = 30f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (pc.inIntro || pc.isDead) return;
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 48.8f));
        rb.AddForce(-transform.up * speed, ForceMode2D.Impulse);
    }
}
