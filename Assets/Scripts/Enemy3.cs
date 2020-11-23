using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D Carrb;
    private float horizontalvelocity; 
    public float carvelocityscaling;
    public float dampening;
    public float radius;
    public float range;
    public float speed;
    public bool CarMode;
    public bool DetectedClose;
    public bool Detected;
    public bool isFiring;
    public bool seePlayer;    
    public Transform player;
    public LayerMask Player;
    public GameObject Car;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Carrb = Car.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectedClose = Physics2D.OverlapCircle(gameObject.transform.position, radius, Player);
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, radius, Player);

        if (DetectedClose)
        {
            rb.AddForce(player.transform.position - transform.position);
            Debug.Log("Detected");
        }
        else
        {
            Debug.Log("Not Detected");
        }

        if (Detected)
        {            
            GetComponent<EnemyGun>().ShootGun();
        }

        if (CarMode == true)
        {
            horizontalvelocity = rb.velocity.x + (Carrb.velocity.x / carvelocityscaling);
        }
        else
        {
            horizontalvelocity = rb.velocity.x;
        }

        horizontalvelocity += rb.velocity.x * speed;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
