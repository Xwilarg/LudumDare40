using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private bool inIntro;

    public Text objectTakeText;
    public GameObject gun;
    public GameObject bullet;
    private int score;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    Camera mainCam;

    private const float speed = 5.0f;
    private const float bulletSpeed = 15.0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        mainCam = Camera.main;
        inIntro = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ItemBase1"))
        {
            Destroy(collision.gameObject);
            score++;
            objectTakeText.text = score.ToString();
        }
    }

    private void Update ()
    {
        if (inIntro) return;
        int horAxis = 0;
        int verAxis = 0;
        Vector3 mouse = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y - transform.position.y));
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 89.5f));
        if (Input.GetButton("Up"))
            verAxis = 1;
        else if (Input.GetButton("Down"))
            verAxis = -1;
        if (Input.GetButton("Left"))
            horAxis = -1;
        else if (Input.GetButton("Right"))
            horAxis = 1;
        if (Input.GetButtonDown("Fire"))
        {
            GameObject bulletIns = Instantiate(bullet, gun.transform.position + transform.forward, Quaternion.identity);
            bulletIns.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(Mathf.Lerp(0, horAxis * speed, 0.8f), Mathf.Lerp(0, verAxis * speed, 0.8f));
    }
}
