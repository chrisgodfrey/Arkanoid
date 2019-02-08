using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public int lives = 3;
    public GameObject ball;

    // Expand
    public Sprite bigShip;
    public Sprite normalShip;

    // Laser
    public Sprite laserShip;
    private bool laserEnabled = false;
    public GameObject pewPew;
    private Vector2 pewPosition;
    public float pewSpeed = 200;

    // Disrupt
    public int disruptBalls = 2;
    private GameObject newBall;
    private Vector2 ballPosition;

    // Catch
    public bool catchEnabled = false;

    // Break
    public GameObject portal;


    void FixedUpdate()
    {
        // Get Horizontal Input
        float h = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * speed;

        // pew pew!
        if (Input.GetKeyDown("space") && laserEnabled == true)
        {
            // spawn a bullet
            GameObject newPew = Instantiate(pewPew);

            // get and use the player's position
            pewPosition.y = gameObject.transform.position.y + 10;
            pewPosition.x = gameObject.transform.position.x;
            // apply player position to the bullet
            newPew.GetComponent<Rigidbody2D>().transform.position = pewPosition;
            // Set starting direction
            Vector2 dir = new Vector2(0, 100).normalized;
            // Set Velocity with dir * speed
            newPew.GetComponent<Rigidbody2D>().velocity = dir * pewSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Did the player hit an Expand pickup?
        if (col.gameObject.tag == "Expand")
        {
            Debug.Log("Picked up Expand!");
            laserEnabled = false;
            // expand the ship!
            gameObject.GetComponent<SpriteRenderer>().sprite = bigShip;
            gameObject.GetComponent<BoxCollider2D>().size = bigShip.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().offset = bigShip.bounds.center;
            // get rid of the pickup
            Destroy(col.gameObject);
            // we only want to be expanded for a limited time
            StartCoroutine(ExpandTime());
        }

        // Did the player hit a Laser pickup?
        if (col.gameObject.tag == "Laser")
        {
            Debug.Log("Picked up Laser!");
            // turn the ship into a shooty blast cannon!
            gameObject.GetComponent<SpriteRenderer>().sprite = laserShip;
            gameObject.GetComponent<BoxCollider2D>().size = laserShip.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().offset = laserShip.bounds.center;
            // get rid of the pickup
            Destroy(col.gameObject);
            // we only want to be expanded for a limited time
            StartCoroutine(LaserTime());
        }

        // Did the player hit a Disrupt pickup?
        if (col.gameObject.tag == "Disrupt")
        {
            Debug.Log("Picked up Disrupt!");
            ball = GameObject.FindWithTag("Ball");
            // get rid of the pickup
            Destroy(col.gameObject);

            // Create two extra balls
            for (int i = 0; i < disruptBalls; i++)
            {
                // spawn a new ball!
                GameObject newBall = Instantiate(ball);
                // get and use the player's Y position
                ballPosition.y = gameObject.transform.position.y + 10;
                ballPosition.x = gameObject.transform.position.x;
                // apply player position to the ball
                ball.GetComponent<Rigidbody2D>().transform.position = ballPosition;
                // Set starting direction
                Vector2 dir = new Vector2(Random.Range(-50, 50), 100).normalized;
                // Set Velocity with dir * speed
                newBall.GetComponent<Rigidbody2D>().velocity = dir * newBall.GetComponent<Ball>().speed;
                // mark the ball as Active
                newBall.GetComponent<Ball>().ballActive = true;
            }
        }

        // Did the player hit a Slow pickup?
        if (col.gameObject.tag == "Slow")
        {
            Debug.Log("Picked up Slow!");
            laserEnabled = false;
            // Slow down the balls!

            // populate an array with all the balls currently in play
            GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("Ball");

            // iterate on each ball, setting the speed
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Ball").Length; i++)
            {
                // Get current direction of ball
                Vector2 dir = activeBalls[i].GetComponent<Rigidbody2D>().velocity;

                float sp = 0.5f;

                // Set Velocity with dir * sp
                activeBalls[i].GetComponent<Rigidbody2D>().velocity = dir * sp;
            }
            // get rid of the pickup
            Destroy(col.gameObject);
        }

        // Did the player hit a Catch pickup?
        if (col.gameObject.tag == "Catch")
        {
            Debug.Log("Picked up Catch!");
            // TODO: Catch a ball
            catchEnabled = true;

            // get rid of the pickup
            Destroy(col.gameObject);
        }

        // Did the player hit a Break pickup?
        if (col.gameObject.tag == "Break")
        {
            Debug.Log("Picked up Break!");
            // get rid of the pickup
            Destroy(col.gameObject);
            // only ever spawn a single portal
            if (GameObject.FindGameObjectsWithTag("Portal").Length == 0)
            {
                // Open a portal to the next level
                Instantiate(portal);
            }
        }

        // Did the racket hit a portal?
        if (col.gameObject.tag == "Portal")
        {
            // Load the next level
            Debug.Log("Loading next level!");
        }

    }

    IEnumerator ExpandTime()
    {
        // set a timer for 15 seconds of elapsed time...
        yield return new WaitForSeconds(15);
        // shrink the ship to normal size
        gameObject.GetComponent<SpriteRenderer>().sprite = normalShip;
        gameObject.GetComponent<BoxCollider2D>().size = normalShip.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().offset = normalShip.bounds.center;
    }

    IEnumerator LaserTime()
    {
        Debug.Log("started laser timer for 15s");
        laserEnabled = true;
        // set a timer for 15 seconds of elapsed time...
        yield return new WaitForSeconds(15);
        // turn the ship into a normal Vaus
        Debug.Log("reverting to normal ship");
        gameObject.GetComponent<SpriteRenderer>().sprite = normalShip;
        gameObject.GetComponent<BoxCollider2D>().size = normalShip.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().offset = normalShip.bounds.center;
        laserEnabled = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Did the racket hit a ball while in Catch mode?
        if (col.gameObject.tag == "Ball" && catchEnabled == true)
        {
            catchEnabled = false;
            col.gameObject.GetComponent<Ball>().ballActive = false;
        }
    }

}