using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pickupExpand;
    public GameObject pickupDisrupt;
    public AudioClip sound;

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
            AudioSource.PlayClipAtPoint(sound,new Vector3(103,136,-5),1f);

            // should this brick drop something?
            int x = Random.Range(0, 3);
            if (x == 0)
            {
                // instantiate an "Expand" pickup
                Instantiate(pickupExpand, transform.position, transform.rotation);
            }
            if (x == 1 && GameObject.FindGameObjectsWithTag("Ball").Length == 1)
            {
                // Instantiate a "Disrupt" pickup if only 1 ball in play
                Instantiate(pickupDisrupt, transform.position, transform.rotation);
            }
            // destroy this brick
            Destroy(gameObject);
        }
    }
}
