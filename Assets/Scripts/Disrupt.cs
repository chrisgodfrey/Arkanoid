using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupt : MonoBehaviour
{
    private GameObject ball;
    private GameObject playerObject;
    private GameObject newBall;
    private Vector2 ballPosition;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ball = GameObject.FindWithTag("Ball");
        playerObject = GameObject.FindWithTag("Vaus");

        // did the pickup hit the player?
        if (col.gameObject.tag == "Vaus")
        {
            Debug.Log("Picked up Disrupt!");

            // run this part twice - I want to create an extra 2 balls for Disrupt
            for (int i = 0; i < 2; i++)
            {
                // spawn a new ball!
                GameObject newBall = Instantiate(ball);

                // get and use the player's Y position
                ballPosition.y = playerObject.transform.position.y + 10;
                ballPosition.x = playerObject.transform.position.x;

                // apply player position to the ball
                GetComponent<Rigidbody2D>().transform.position = ballPosition;

                // Set starting direction
                Vector2 dir = new Vector2(50, 100).normalized;

                // Set Velocity with dir * speed
                newBall.GetComponent<Rigidbody2D>().velocity = dir * newBall.GetComponent<Ball>().speed;

                // mark the ball as Active
                newBall.GetComponent<Ball>().ballActive = true;
            }
            // get rid of the pickup
            Destroy(gameObject);
        }
    }
}