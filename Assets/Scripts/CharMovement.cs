using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float dampening;
    public float jumprange;
    public float jumpForce;
    public float maxVelocity;
    public LayerMask Walkablesurfaces;
    private float groundtimer;
    public GameObject Car;
    private Rigidbody2D Carrb;
    public float carvelocityscaling;
    public bool CarMode;
    private float horizontalvelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Carrb = Car.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(CarMode == true)
        {
            horizontalvelocity = rb.velocity.x + (Carrb.velocity.x / carvelocityscaling);
        }
        else
        {
            horizontalvelocity = rb.velocity.x;
        }

        horizontalvelocity += Input.GetAxisRaw("Horizontal") * speed;
        if (!Input.GetMouseButton(1) || Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalvelocity *= Mathf.Pow(0.5f, Time.deltaTime * dampening);
        }

        rb.velocity = new Vector2(horizontalvelocity, rb.velocity.y);

        if(groundtimer < 1)
        {
            groundtimer += Time.deltaTime;
        }

        if(groundtimer >= 0.1)
        {
            if (Input.GetAxisRaw("Vertical") != 0 && CheckGround() == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                groundtimer = 0;
            }
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
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

    public bool CheckGround()
    {
        Vector2 Pos = (Vector2)transform.position + new Vector2(0, -jumprange);
        Vector2 Scale = (Vector2)transform.localScale + new Vector2(jumprange, 0);
        bool Grounded = Physics2D.OverlapBox(Pos, Scale, 0, Walkablesurfaces);
        return Grounded;
    }

    void OnDrawGizmos()
    {
        Vector2 Pos = (Vector2)transform.position + new Vector2(0, -jumprange);
        Vector2 Scale = (Vector2)transform.localScale + new Vector2(jumprange, 0);
        Gizmos.color = Color.red;
        if (Input.GetAxisRaw("Vertical") != 0)
            Gizmos.DrawWireCube(Pos, Scale);
    }
}

