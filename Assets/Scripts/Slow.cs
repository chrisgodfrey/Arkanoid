using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
