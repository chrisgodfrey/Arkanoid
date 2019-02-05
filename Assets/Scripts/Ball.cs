using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    public GameObject playerObject;
    public bool ballActive;
    public Vector3 ballPosition;
    public GameObject vausLife3;
    public GameObject vausLife2;
    public bool gameOver;
    public AudioClip hitVaus;
 
    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    // Use this for initialization
    void Start()
    {
        gameOver = false;

        // get and use the player's Y position
        ballPosition.y = playerObject.transform.position.y + 10;

        // apply player Y position to the ball
        GetComponent<Rigidbody2D>().transform.position = ballPosition;
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

                    // play 'hit vaus' sound
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
                // is this the last ball in play?
                if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
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
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            // game over!
            Destroy(playerObject);
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
    }
}