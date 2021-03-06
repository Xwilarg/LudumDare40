﻿using UnityEngine;

public class moveDoor : MonoBehaviour {
    
    private Rigidbody2D rb;
    public bool moveAtStart;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (moveAtStart)
            move();
    }

    public void move()
    {
        rb.AddForce(new Vector2(-300, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StopLaser"))
            rb.velocity = Vector2.zero;
    }
}
