using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    public float speed = 30;
    public string axis = "Horizontal";

    void FixedUpdate ()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(v, 0) * speed;
    }
}
