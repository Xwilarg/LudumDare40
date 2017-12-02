using UnityEngine;

public class LaunchCrate : MonoBehaviour {

    public GameObject player;
    public GameObject projectile;
    private Rigidbody2D rb;
    private PlayerController pc;
    float refTime;
    float currTime;

    private const float speed = 30f;

    private void Start()
    {
        int diff = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().difficulty;
        if (diff == 1) refTime = 3f;
        else if (diff == 2) refTime = 2f;
        else if (diff == 3) refTime = 1f;
        currTime = 0f;
        rb = GetComponent<Rigidbody2D>();
        pc = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        currTime += Time.deltaTime;
        if (pc.inIntro || pc.isDead) return;
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 48.65f));
        if (currTime > refTime)
        {
            currTime = 0f;
            GameObject crate = Instantiate(projectile, transform.position - transform.up / 4, Quaternion.identity);
            crate.GetComponent<Rigidbody2D>().AddForce(-transform.up * 1000, ForceMode2D.Impulse);
        }
    }
}
