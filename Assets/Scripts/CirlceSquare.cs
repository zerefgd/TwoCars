using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirlceSquare : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    void Start()
    {
        speed = 10f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,- speed);
    }
}
