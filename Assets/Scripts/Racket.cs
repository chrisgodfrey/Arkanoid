using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public int lives = 3;
    public GameObject ball;
    Animator anim;
    int deadHash = Animator.StringToHash("Dead");


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;

        // if (ball.GetComponent<Ball>().gameOver == true)
        // {
        //     //anim.SetTrigger(deadHash);
        // }
    }
}