using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public GameObject racket;
    public Sprite bigShip;
    public Sprite normalShip;

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
        racket = GameObject.FindWithTag("Vaus");
        // did the pickup hit the player?
        if (col.gameObject.tag == "Vaus")
        {
            Debug.Log("Picked up Expand!");
            // expand the ship!
            racket.GetComponent<SpriteRenderer>().sprite = bigShip;
            racket.GetComponent<BoxCollider2D>().size = bigShip.bounds.size;
            racket.GetComponent<BoxCollider2D>().offset = bigShip.bounds.center;
            // get rid of the pickup
            Destroy(gameObject);
            // we only want to be expanded for a limited time
            StartCoroutine(ExpandTime());
        }
    }

    IEnumerator ExpandTime()
    {
        // set a timer for 15 seconds of elapsed time...
        yield return new WaitForSeconds(15);
        // shrink the ship to normal size
        racket.GetComponent<SpriteRenderer>().sprite = normalShip;
        racket.GetComponent<BoxCollider2D>().size = normalShip.bounds.size;
        racket.GetComponent<BoxCollider2D>().offset = normalShip.bounds.center;
    }
}
