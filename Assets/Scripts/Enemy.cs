using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool inCar = true;
    public float speed;
    public float health;
    public float range;
    public bool Detected;
    public LayerMask Car;
    public Transform car;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, range, Car);

        if (Detected == false && inCar == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(car.position.x, car.position.y), speed * Time.deltaTime);
            Vector2 direction = car.transform.position - transform.position * speed * Time.deltaTime;
            //Debug.Log("direction");
        }
        //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);     
        if (gameObject.transform.parent != null)
        {
            inCar = true;
        }
        else
        {
            inCar = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
