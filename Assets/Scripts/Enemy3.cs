using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalvelocity;
    private Rigidbody2D Carrb;
    public float carvelocityscaling;
    public float dampening;
    public float range;
    public float radius;
    public float speed;
    public bool seePlayer;
    public bool CarMode;
    public Transform player;
    public GameObject Car;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Carrb = Car.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, radius, player);

        if (Detected)
        {
            rb.AddForce(player.transform.position - transform.position);
            Debug.Log("Move Left");
        }
        else
        {
            Debug.Log("Move Right");
        }

        if (CarMode == true)
        {
            horizontalvelocity = rb.velocity.x + (Carrb.velocity.x / carvelocityscaling);
        }
        else
        {
            horizontalvelocity = rb.velocity.x;
        }

        horizontalvelocity += transform.position.x * speed;
        horizontalvelocity *= Mathf.Pow(0.5f, Time.deltaTime * dampening);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CarMode = false;
        }
        if (collision.gameObject.tag == "Car")
        {
            CarMode = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
