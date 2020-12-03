using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public int damage;
    public float lifetime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.right * speed;

        //lifetime -= Time.deltaTime;

        //if(lifetime <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "Car")
        {
            collision.gameObject.GetComponent<Moto>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
