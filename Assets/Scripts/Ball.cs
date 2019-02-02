using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    public GameObject player;
    public bool ballActive;

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth)
    {
        // ascii art:
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    // Use this for initialization
    void Start()
    {
        ballActive = false;
    }

    void FixedUpdate()
    {
        if (ballActive == false && player != null)
        {
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.name == "racket")
        {
            // Calculate hit Factor
            float x = hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}
