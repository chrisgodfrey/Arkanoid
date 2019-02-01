using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ballActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;
    public PlayerController PlayerController;


    // Start is called before the first frame update
    void Start()
    {
        ballInitialForce = new Vector2(100.0f, 300.0f);

        // set to inactive
        ballActive = false;

        // ball position
        ballPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check for user input
        if (Input.GetButtonDown("Jump") == true)
        {
            // check if is the first play
            if (!ballActive)
            {
                // add a force
                GetComponent<Rigidbody2D>().AddForce(ballInitialForce);
                // set ball active
                ballActive = !ballActive;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D Bottom)
    {
        Destroy(gameObject);
        PlayerController.lives -= 1;
    }
}
