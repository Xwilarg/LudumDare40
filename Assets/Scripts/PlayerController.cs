using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour {
    private int addForce;

    public GameObject runEnemy;
    public GameObject gameOver;

    private const float refTimeDead = 0.5f;
    private float currTimeDead;

    private const float refTime = 20f;
    private float currTime;

    public Text timer;

    public bool inIntro { set; get; }
    public bool isDead { set; get; }
    private bool isPause;

    private const float refReload = 0.05f;
    private float currReload;

    private const float refReload2 = 0.5f;
    private float currReload2;

    public GameObject pause { set; private get; }
    public GameObject popup { set; get; }
    public MakeTemporary pop { set; private get; }

    public float magnetForce { private set; get; }
    public float angleDeriv { private set; get; }

    public Text objectTakeText;
    public Text relicTakeText;
    public GameObject gun;
    public GameObject bullet;
    public GameObject bullet2;
    private int score;
    public GameObject dark1, dark2, dark3, dark4;
    private int darkCounter;
    public Text deathText;

    private bool secondWeapon;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    Camera mainCam;
    private int diff;
    public bool isNetwork { private set; get; }
    private Vector2 impulse;

    private string up, down, left, right;

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
        currTime = -1f;
        currTimeDead = -1f;
        angleDeriv = 0f;
        secondWeapon = true;
        currReload = 0f;
        magnetForce = 0f;
        up = "Up";
        down = "Down";
        left = "Left";
        right = "Right";
        impulse = Vector2.zero;
        if (SceneManager.GetActiveScene().name == "DeathScene" || SceneManager.GetActiveScene().name == "MultiScene" || SceneManager.GetActiveScene().name == "RandomScene")
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
        isNetwork = (GetComponent<NetworkIdentity>() != null);
        isPause = false;
    }

    private void swap(ref string a, ref string b)
    {
        string t = a;
        a = b;
        b = t;
    }

    private void changeCommand()
    {
        int randomNb = Random.Range(0, 6);
        if (randomNb == 0)
            swap(ref up, ref down);
        else if (randomNb == 1)
            swap(ref up, ref left);
        else if (randomNb == 2)
            swap(ref up, ref right);
        else if (randomNb == 3)
            swap(ref down, ref left);
        else if (randomNb == 4)
            swap(ref down, ref right);
        else if (randomNb == 5)
            swap(ref left, ref right);
    }

    private void increaseDeriv()
    {
        angleDeriv += 0.5f;
    }

    private void disableAllLasers()
    {
        GameObject[] all = FindObjectsOfType<GameObject>();
        //List<GameObject> lasers = new List<GameObject>();
        foreach (GameObject go in all)
        {
            if (go.name.Length >= 5 && go.name.Substring(0, 5) == "Laser")
                go.GetComponent<SpriteRenderer>().enabled = false;
        }
        /*for (int i = 0; i < cd.difficulty; i++)
        {
            int random = Random.Range(0, lasers.Count);
            lasers[random].GetComponent<SpriteRenderer>().enabled = false;
            lasers.RemoveAt(random);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ItemBase1"))
        {
            popup.SetActive(true);
            switch ((int)collision.gameObject.GetComponent<PowerDown>().pde)
            {
                case 0: addForce++; pop.reset("<b>SHAKE</b>\n\nDon't stay too close to lasers."); break;
                case 1: hideNext(); pop.reset("<b>CROP SCREEN</b>\n\nNom nom nom."); break;
                case 3: changeCommand(); pop.reset("<b>CHANGE KEYS</b>\n\nHere's your new config:"
                    + "\nUp: " + up + ", Down: " + down + ", Left: " + left + ", Right: " + right); break;
                case 4: increaseDeriv(); pop.reset("<b>AIM</b>\n\nNot the best day for sniping."); break;
                case 5: pop.reset("<b>NONE</b>"); break;
                case 6: if (secondWeapon) { secondWeapon = false; pop.reset("<b>NO SECONDARY GUN</b>\n\nOnly main weapon now."); } break;
                case 7: disableAllLasers(); pop.reset("<b>LASER VISION</b>\n\nYou can't see them but they are still here."); break;
                case 8: GameObject player = Instantiate(gameObject, new Vector2(-1.268f, -4.23f), Quaternion.identity);
                    PlayerController pc2 = player.GetComponent<PlayerController>();
                    pc2.pause = pause; pc2.popup = popup; pc2.pop = pop;
                    pop.reset("<b>DUPLICATE</b>\n\nWhich one is the real one ? They both are!"); break;
                case 9:
                    currTime = 0f;
                    timer.color = Color.red;
                    pop.reset("<b>TIMER</b>\n\n20 seconds left."); break;
                case 10: GameObject enemy = Instantiate(runEnemy, transform.position + transform.up, Quaternion.identity);
                    enemy.GetComponent<KillPlayer>().player = gameObject; MoveLaser mv = enemy.GetComponentInChildren<MoveLaser>();
                    mv.gameOver = gameOver; mv.pc = this;
                    pop.reset("<b>TRAP</b>\n\n Surprise!"); break;
                case 2:
                    if (cd.levelPlaying == 1)
                        SceneManager.LoadScene("EasterEgg");
                    else if (cd.levelPlaying == 2)
                        SceneManager.LoadScene("EasterEgg2");
                    break;
                default: break;
            }
            Destroy(collision.gameObject);
            if (cd.levelPlaying == 3)
                cd.score += 10;
            score = objectTakeText.text[0] - '0' + 1;
            objectTakeText.text = score.ToString() + "/8";
            if (score == 8 && (cd.levelPlaying < 4 || cd.levelPlaying == 7))
            {
                if (cd.levelPlaying == 7)
                    SceneManager.LoadScene("Victory");
                else if (cd.levelPlaying == 3)
                {
                    cd.score += 250;
                    SceneManager.LoadScene("DeathScene");
                }
                else
                {
                    SceneManager.LoadScene("Victory");
                    cd.increaseFile(1);
                    cd.currLevel = 1;
                }
            }
        }
        else if (collision.collider.CompareTag("ItemSup"))
        {
            Destroy(collision.gameObject);
            relicTakeText.text = "1/1";
        }
        else if ((collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("Bullet2") || collision.collider.CompareTag("BulletFreeze")) && collision.collider.GetComponent<DeleteCollision>().owner != gameObject)
        {
            float angle = Mathf.Atan2(collision.transform.position.y - transform.position.y,
                collision.transform.position.x - transform.position.x);
            impulse = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 89.5f)) * Vector2.up * -10;
            Destroy(collision.gameObject);
        }
    }

    public void resetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    private void createBullet(Vector3 up)
    {
        GameObject bulletIns = Instantiate(bullet, transform.position + up / 2, Quaternion.identity);
        bulletIns.GetComponent<DeleteCollision>().owner = gameObject;
        NetworkServer.Spawn(bulletIns);
        // if (isServer)
        //   NetworkServer.SpawnWithClientAuthority(bulletIns, connectionToClient);
        Vector2 force = up * 1500;
        //RpcLaunchBullet(bulletIns, force);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private void launchBullet(GameObject go, Vector2 force)
    {
        go.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

     [ClientRpc]
     private void RpcLaunchBullet(GameObject go, Vector3 force)
     {
         launchBullet(go, force);
     }

    [Command]
    private void CmdCreateBullet(Vector3 up)
    {
        createBullet(up);
    }

    private void Update ()
    {
        if (currTimeDead >= refTimeDead)
        {
            cd.score -= 100;
            SceneManager.LoadScene("DeathScene");
        }
        else if (currTimeDead >= 0f)
            currTimeDead += Time.deltaTime;
        if (currTime >= refTime)
        {
            gameOver.SetActive(true);
            isDead = true;
            resetVelocity();
            currTime = -1f;
            currTimeDead = 0f;
        }
        else if (currTime >= 0f)
        {
            currTime += Time.deltaTime;
            timer.text = (20 - currTime).ToString("F") + " seconds remaining";
        }
        if (currReload > 0f)
            currReload -= Time.deltaTime;
        if (currReload2 > 0f)
            currReload2 -= Time.deltaTime;
        if (isPause)
        {
            if (Input.GetButtonDown("Pause"))
            {
                pause.SetActive(false);
            }
            return;
        }
        if (inIntro || isDead || (isNetwork && !isLocalPlayer)) return;
        float horAxis = Random.Range(-.1f * diff, .1f * diff) * addForce;
        float verAxis = Random.Range(-.1f * diff, .1f * diff) * addForce;
        Vector3 mouse = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y - transform.position.y));
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 89.5f));
        if (Input.GetButton(up))
            verAxis += 1f;
        else if (Input.GetButton(down))
            verAxis += -1f;
        if (Input.GetButton(left))
            horAxis += -1f;
        else if (Input.GetButton(right))
            horAxis += 1f;
        if (Input.GetButtonDown("Fire") && currReload <= 0f)
        {
            if (isNetwork)
            {
                currReload = refReload;
                if (isServer)
                    createBullet(transform.up);
                else
                    CmdCreateBullet(transform.up);
            }
            else
            {
                GameObject bulletIns = Instantiate(bullet, gun.transform.position, Quaternion.identity);
                bulletIns.GetComponent<Rigidbody2D>().AddForce((transform.up + new Vector3(Random.Range(-angleDeriv, angleDeriv), Random.Range(-angleDeriv, angleDeriv), 0f)) * bulletSpeed, ForceMode2D.Impulse);
                bulletIns.GetComponent<DeleteCollision>().owner = gameObject;
            }
        }
        else if (!isNetwork && Input.GetButtonDown("Fire2") && currReload2 <= 0f && secondWeapon)
        {
            currReload2 = refReload2;
            GameObject bulletIns = Instantiate(bullet2, gun.transform.position, Quaternion.identity);
            bulletIns.GetComponent<Rigidbody2D>().AddForce((transform.up + new Vector3(Random.Range(-angleDeriv, angleDeriv), Random.Range(-angleDeriv, angleDeriv), 0f)) * bulletSpeed / 3, ForceMode2D.Impulse);
            bulletIns.GetComponent<DeleteCollision>().owner = gameObject;
        }
        if (pause != null && Input.GetButtonDown("Pause"))
        {
            pause.SetActive(true);
            isPause = true;
        }
        rb.velocity = new Vector2(Mathf.Lerp(0, horAxis * speed, 0.8f), Mathf.Lerp(0, verAxis * speed, 0.8f)) + impulse;
        if (Mathf.Abs((rb.velocity.x + rb.velocity.y) / 2) < 1f || Mathf.Abs((impulse.x + impulse.y) / 2) < 1f)
        {
            impulse.x = 0.0f;
            impulse.y = 0.0f;
        }
        if (impulse.x > 0f)
            impulse.x -= Time.deltaTime * 10;
        else if (impulse.x < 0f)
            impulse.x += Time.deltaTime * 10;
        if (impulse.y > 0f)
            impulse.y -= Time.deltaTime * 10;
        else if (impulse.y < 0f)
            impulse.y += Time.deltaTime * 10;
    }

    public void removePause()
    {
        isPause = false;
    }
}
