using UnityEngine;

public class MagnetPlayer : MonoBehaviour {

    private Rigidbody2D rb;
    public PlayerController pc { set; private get; }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        /*float angle = Mathf.Atan2(pc.transform.position.y - transform.position.y,
                pc.transform.position.x - transform.position.x);
        rb.AddForce(Quaternion.Euler(0, 0, (180 / Mathf.PI) * (angle - 89.5f)) * Vector2.up * pc.magnetForce);*/
        Vector2 vel;
        if (pc.transform.position.x < transform.position.x)
            vel.x = -pc.magnetForce;
        else
            vel.x = pc.magnetForce;
        if (pc.transform.position.y < transform.position.y)
            vel.y = pc.magnetForce;
        else
            vel.y = -pc.magnetForce;
        //rb.velocity = Vector2.Scale(vel, rb.velocity);
    }
}
