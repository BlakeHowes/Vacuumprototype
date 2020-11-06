using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D rb;
    public float boost;
    public float thrust;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.TransformDirection(-Vector3.up)*boost);
    }

    void Update()
    {
        
    }
}
