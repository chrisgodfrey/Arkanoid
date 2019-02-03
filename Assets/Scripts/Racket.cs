using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public int lives = 3;
    public Sprite bigShip;
    public Sprite normalShip;

    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Hit the Racket?
        if (col.gameObject.tag == "Expand")
        {
            Debug.Log("The paddle hit the pickup!");
            GetComponent<SpriteRenderer>().sprite = bigShip;
        }
    }
}