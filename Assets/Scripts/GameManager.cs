using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highscore;
    public GameObject scoreText;
    public GameObject highScoreText;
    public int lives = 3;
    public AudioClip startMusic;
    public GameObject playerObject;
    public GameObject vausLife3;
    public GameObject vausLife2;
    public Animator animPlayer;
    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highscore = 0;
        // play starting music! yeah!!
        GetComponent<AudioSource>().PlayOneShot(startMusic, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int addScore)
    {
        score = (score + addScore);
        scoreText.GetComponent<TextMeshProUGUI>().text = (score.ToString());
    }

    public void LoseLife()
    {
        Debug.Log("you lost a life!");
        lives = lives - 1;

        // Destroy all falling pickups
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Expand");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Expand").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy all falling pickups
        pickups = GameObject.FindGameObjectsWithTag("Disrupt");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Disrupt").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy all falling pickups
        pickups = GameObject.FindGameObjectsWithTag("Laser");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Laser").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy all falling pickups
        pickups = GameObject.FindGameObjectsWithTag("Catch");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Catch").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy all falling pickups
        pickups = GameObject.FindGameObjectsWithTag("Break");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Break").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy all falling pickups
        pickups = GameObject.FindGameObjectsWithTag("Slow");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Slow").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // Destroy portal
        pickups = GameObject.FindGameObjectsWithTag("Portal");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Portal").Length; i++)
        {
            Destroy(pickups[i]);
        }

        // reset player ship to normal 
        // TODO

        Debug.Log("Player Lives = " + lives);

        if (lives > 0)
        {
            if (lives == 2)
            {
                vausLife3.SetActive(false);
            }
            if (lives == 1)
            {
                vausLife2.SetActive(false);
            }
            
        // reset racket position
        playerObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(84, 16));

        // disable pickup effects
        playerObject.GetComponent<Racket>().laserEnabled = false;
        playerObject.GetComponent<Racket>().expandEnabled = false;
        

        }
        else
        {
            Debug.Log("you lost all your lives!");

        // blow up the ship
        animPlayer = playerObject.GetComponent<Animator>();
        animPlayer.SetBool ("PlayerDead", true);

        // remove the ball
        ball = GameObject.FindGameObjectWithTag("Ball");
        Destroy(ball);

        // display "game over press any key" message <- coroutine?

        // go back to main menu

        }
    }
}