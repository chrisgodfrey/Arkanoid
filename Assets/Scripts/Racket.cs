using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Movement Speed
    public float speed = 150;
    public GameObject ball;

    // Animation
    private Animator animPlayer;

    // Laser
    public Sprite laserShip;
    public bool laserEnabled = false;
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

    // Expand
    public bool expandEnabled = false;


    void Start()
    {
        animPlayer = gameObject.GetComponent<Animator>();
    }

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
            if (laserEnabled == true)
            {
                animPlayer.SetBool("PlayerLaser", false);
                laserEnabled = false;
            }
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(48, 8);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(24, 4);
            // expand the ship!
            animPlayer.SetBool("PlayerExpanded", true);
            expandEnabled = true;
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
            animPlayer.SetBool("PlayerLaser", true);
            laserEnabled = true;
            if (expandEnabled == true)
            {
                animPlayer.SetBool("PlayerExpanded", false);
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(16, 4);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(32, 8);
            }
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
        // set a timer for 10 seconds of elapsed time...
        yield return new WaitForSeconds(10);
        // shrink the ship to normal size
        animPlayer.SetBool("PlayerExpanded", false);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(16, 4);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(32, 8);
        expandEnabled = false;
    }

    IEnumerator LaserTime()
    {
        // set a timer for 10 seconds of elapsed time...
        yield return new WaitForSeconds(10);
        // make the ship not be a shooty pewpew gunboat
        animPlayer.SetBool("PlayerLaser", false);
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