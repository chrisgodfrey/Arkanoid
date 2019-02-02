using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public int lives = 3;

    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;
    }

    void Update()
    {
        if (lives == 0)
        {
            Debug.Log("you lost all your lives!");
            Destroy(gameObject);
        }
    }
}
