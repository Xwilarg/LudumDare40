using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private int addForce;

    public bool inIntro { set; get; }
    public bool isDead { set; get; }

    public Text objectTakeText;
    public Text relicTakeText;
    public GameObject gun;
    public GameObject bullet;
    private int score;
    public GameObject dark1, dark2, dark3, dark4;
    private int darkCounter;
    public Text deathText;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    Camera mainCam;
    private int diff;

    private const float speed = 5.0f;
    private const float bulletSpeed = 15.0f;

    private CountDeath cd;

    public void hideNext()
    {
        darkCounter++;
        if (darkCounter == 1) dark1.SetActive(true);
        else if (darkCounter == 2) dark2.SetActive(true);
        else if (darkCounter == 3) dark3.SetActive(true);
        else if (darkCounter == 4) dark4.SetActive(true);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "DeathScene" || SceneManager.GetActiveScene().name == "MultiScene")
            inIntro = false;
        else
            inIntro = true;
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
        diff = cd.difficulty;
        if (deathText != null)
            deathText.text = cd.nbDeath.ToString();
        darkCounter = 0;
        addForce = 0;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        mainCam = Camera.main;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ItemBase1"))
        {
            switch ((int)collision.gameObject.GetComponent<PowerDown>().pde)
            {
                case 0: addForce++; break;
                case 1: hideNext(); break;
                default: break;
            }
            Destroy(collision.gameObject);
            score++;
            objectTakeText.text = score.ToString() + "/8";
            if (score == 8)
            {
                SceneManager.LoadScene("Victory");
                cd.increaseFile(1);
            }
        }
        else if (collision.collider.CompareTag("ItemSup"))
        {
            Destroy(collision.gameObject);
            relicTakeText.text = "1/1";
        }
    }

    public void resetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    private void Update ()
    {
        if (inIntro || isDead) return;
        float horAxis = Random.Range(-.1f * diff, .1f * diff) * addForce;
        float verAxis = Random.Range(-.1f * diff, .1f * diff) * addForce;
        Vector3 mouse = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y - transform.position.y));
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 89.5f));
        if (Input.GetButton("Up"))
            verAxis += 1f;
        else if (Input.GetButton("Down"))
            verAxis += -1f;
        if (Input.GetButton("Left"))
            horAxis += -1f;
        else if (Input.GetButton("Right"))
            horAxis += 1f;
        if (Input.GetButtonDown("Fire"))
        {
            GameObject bulletIns = Instantiate(bullet, gun.transform.position, Quaternion.identity);
            bulletIns.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(Mathf.Lerp(0, horAxis * speed, 0.8f), Mathf.Lerp(0, verAxis * speed, 0.8f));
    }
}
