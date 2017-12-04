using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLaser : MonoBehaviour {

    public GameObject gameOver;
    private Rigidbody2D rb;
    public PlayerController pc;
    public float refTime { set; get; }
    public float currTime { set; get; }
    private CountDeath cd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        refTime = -1f;
        currTime = 0f;
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
    }

    private void Update()
    {
        if (refTime == -1f) return;
        currTime += Time.deltaTime;
        if (currTime > refTime)
        {
            cd.score -= 100;
            SceneManager.LoadScene("DeathScene");
        }
    }

    public void move()
    {
        rb.AddForce(new Vector2(100, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StopLaser"))
            rb.velocity = Vector2.zero;
        else if (collision.CompareTag("Player"))
        {
            PlayerController tmpPc = collision.GetComponent<PlayerController>();
            if (SceneManager.GetActiveScene().name == "EasterEgg" || SceneManager.GetActiveScene().name == "EasterEgg2")
                collision.gameObject.transform.position = new Vector2(0f, 0f);
            else if (tmpPc.isNetwork)
                collision.gameObject.transform.position = Vector2.zero;
            else if (cd.levelPlaying == 7)
                collision.gameObject.transform.position = new Vector2(-1.268f, -4.23f);
            else
            {
                gameOver.SetActive(true);
                pc.isDead = true;
                pc.resetVelocity();
                refTime = .5f;
            }
        }
    }
}
