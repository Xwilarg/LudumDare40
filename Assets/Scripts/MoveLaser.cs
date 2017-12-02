using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour {

    public GameObject gameOver;
    private Rigidbody2D rb;
    public PlayerController pc;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            gameOver.SetActive(true);
            pc.isDead = true;
            pc.resetVelocity();
        }
    }
}
