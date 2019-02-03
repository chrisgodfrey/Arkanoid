using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    public GameObject playerObject;
    public GameObject gameManager;
    public bool ballActive;
    public Vector3 ballPosition;
    public GameObject vausLife3;
    public GameObject vausLife2;
    public bool gameOver;
    public AudioClip startMusic;
    public AudioClip hitBrick;
    public AudioClip hitVaus;
    public GameObject pickupExpand;

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
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
        gameOver = false;

        // get and use the player's Y position
        ballPosition.y = playerObject.transform.position.y + 10;

        // apply player Y position to the ball
        GetComponent<Rigidbody2D>().transform.position = ballPosition;

        // play starting music! yeah!!
        GetComponent<AudioSource>().PlayOneShot(startMusic, 1);
    }

    void Update()
    {
        if (gameOver == false)
        {
            if (Input.GetKeyDown("space"))
            {
                if (ballActive == false)
                {
                    // Set starting direction
                    Vector2 dir = new Vector2(50, 100).normalized;

                    // Set Velocity with dir * speed
                    GetComponent<Rigidbody2D>().velocity = dir * speed;

                    // mark the ball as Active
                    ballActive = true;

                    // play 'hit vaus' sound <- this is not the issue dude
                    GetComponent<AudioSource>().PlayOneShot(hitVaus, 1);
                }
            }

            if (ballActive == false && playerObject != null)
            {
                // get and use the player position
                ballPosition.x = (playerObject.transform.position.x + 20);
                ballPosition.y = (playerObject.transform.position.y + 12);

                // apply player position to the ball
                GetComponent<Rigidbody2D>().transform.position = ballPosition;
            }

            if (GetComponent<Rigidbody2D>().transform.position.y < 0 && playerObject.GetComponent<Racket>().lives > 0)
            {
                Debug.Log("you lost a life!");
                ballActive = false;
                // decrease player lives by 1
                playerObject.GetComponent<Racket>().lives -= 1;

                // reset racket position
                playerObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(84, 16));

                Debug.Log("Player Lives = " + playerObject.GetComponent<Racket>().lives);

                if (playerObject.GetComponent<Racket>().lives > 0)
                {
                    // play starting music! yeah!!
                    GetComponent<AudioSource>().PlayOneShot(startMusic, 1);
                    if (playerObject.GetComponent<Racket>().lives == 2)
                    {
                        vausLife3.SetActive(false);
                    }
                    if (playerObject.GetComponent<Racket>().lives == 1)
                    {
                        vausLife2.SetActive(false);
                    }
                }
                else
                {
                    Debug.Log("you lost all your lives!");
                    gameOver = true;
                }
            }
        }
        else
        {
            // game over!
            Destroy(playerObject);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.name == "racket")
        {
            // play 'hit vaus' sound
            GetComponent<AudioSource>().PlayOneShot(hitVaus, 1);

            // Calculate hit Factor
            float x = hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit a brick?
        if (col.gameObject.tag == "Brick")
        {
            // increase the score
            gameManager.GetComponent<GameManager>().UpdateScore(100);

            // play 'hit brick' sound
            GetComponent<AudioSource>().PlayOneShot(hitBrick, 1);

            // should this brick drop something?
            int x = Random.Range(0,3);
            if (x == 0)
            {
                // instantiate an "Expand" pickup
                Instantiate(pickupExpand, transform.position, transform.rotation);
            }
            // remove the brick that we hit
            Destroy(col.gameObject);
        }
    }
}