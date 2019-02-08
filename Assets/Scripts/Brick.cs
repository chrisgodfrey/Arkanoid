﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pickupExpand;
    public GameObject pickupDisrupt;
    public GameObject pickupLaser;
    public GameObject pickupSlow;

    public AudioClip sound;
    private int brickHealth = 4;

    void Start()
    {
        // attach the GameManager script to this prefab
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Did a ball hit this brick?
        if (col.gameObject.tag == "Ball")
        {
            // increase the score
            gameManager.GetComponent<GameManager>().UpdateScore(100);

            // spawn an AudioSource at origin 
            // then dispose of it when the sound has played
            // need this weird behaviour because the brick will be destroyed immediately
            // which would also destroy any audiosource components on this object
            // and make the sound not play
            // cameraZpos is needed to get the sound to play close to the camera
            // otherwise its way too quiet and we can't hear it
            AudioSource.PlayClipAtPoint(sound, new Vector3(103, 136, -5), 1f);

            // should this brick drop something?
            int x = Random.Range(0, 5);
            if (x == 0)
            {
                // instantiate an "Expand" pickup if ship is not currently expanded
                Instantiate(pickupExpand, transform.position, transform.rotation);
            }
            if (x == 1 && GameObject.FindGameObjectsWithTag("Ball").Length == 1 && GameObject.FindGameObjectsWithTag("Disrupt").Length == 0)
            {
                // Instantiate a "Disrupt" pickup if only 1 ball in play
                Instantiate(pickupDisrupt, transform.position, transform.rotation);
            }
            if (x == 2)
            {
                // Instantiate a "Laser" pickup
                Instantiate(pickupLaser, transform.position, transform.rotation);
            }
            if (x == 3)
            {
                // Instantiate a "Slow" pickup
                Instantiate(pickupSlow, transform.position, transform.rotation);
            }
            // destroy this brick
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Did a laser hit this brick?
        if (col.gameObject.tag == "PewPew")
        {
            // Destroy the laser
            Destroy(col.gameObject);

            // decrease the health of the brick
            brickHealth = brickHealth - 1;
            if (brickHealth < 1)
            {
                // increase the score
                gameManager.GetComponent<GameManager>().UpdateScore(100);
                // Destroy the brick
                Destroy(gameObject);
            }
        }
    }
}
