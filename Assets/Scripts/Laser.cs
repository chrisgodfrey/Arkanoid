using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject racket;
    public Sprite laserShip;
    public Sprite normalShip;
    private bool laserActive = false;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().transform.position.y < 0 && laserActive == false)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        racket = GameObject.FindWithTag("Vaus");
        // did the pickup hit the player?
        if (col.gameObject.tag == "Vaus")
        {
            Debug.Log("Picked up Laser!");
            // turn the ship into a shooty blast cannon!
            racket.GetComponent<SpriteRenderer>().sprite = laserShip;
            racket.GetComponent<BoxCollider2D>().size = laserShip.bounds.size;
            racket.GetComponent<BoxCollider2D>().offset = laserShip.bounds.center;
            laserActive = true;
            // we only want to be expanded for a limited time
            StartCoroutine(LaserTime());
        }
    }

    IEnumerator LaserTime()
    {
        Debug.Log("started laser timer for 15s");
        // set a timer for 15 seconds of elapsed time...
        yield return new WaitForSeconds(15);
        // turn the ship into a normal Vaus
        Debug.Log("reverting to normal ship");
        racket.GetComponent<SpriteRenderer>().sprite = normalShip;
        racket.GetComponent<BoxCollider2D>().size = normalShip.bounds.size;
        racket.GetComponent<BoxCollider2D>().offset = normalShip.bounds.center;
        laserActive = false;
    }
}
