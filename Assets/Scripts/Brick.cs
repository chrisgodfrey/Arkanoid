using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip hitBrick;
    public GameObject pickupExpand;
    public GameObject pickupDisrupt;

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

            // play 'hit vaus' sound
            GetComponent<AudioSource>().PlayOneShot(hitBrick, 1);

            // should this brick drop something?
            int x = Random.Range(0, 3);
            if (x == 0)
            {
                // instantiate an "Expand" pickup
                Instantiate(pickupExpand, transform.position, transform.rotation);
            }
            if (x == 1)
            {
                // Instantiate a "Disrupt" pickup
                Instantiate(pickupDisrupt, transform.position, transform.rotation);
            }
            // destroy this brick
            Destroy(gameObject);
        }
    }
}
