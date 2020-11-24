using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float dampening;
    public float jumprange;
    public float wallrange;
    public float jumpForce;
    public float maxVelocity;
    public LayerMask Walkablesurfaces;
    public LayerMask CarWalls;
    private float groundtimer;
    public GameObject Car;
    private Rigidbody2D Carrb;
    public float carvelocityscaling;
    public bool CarMode;
    private float horizontalvelocity;
    public bool spacemode;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Car != null)
        {
            Carrb = Car.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if(CarMode == false)
        {
            horizontalvelocity = rb.velocity.x;
        }
        else
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                horizontalvelocity = rb.velocity.x + (Carrb.velocity.x / carvelocityscaling);
            }
            else
            {
                horizontalvelocity = rb.velocity.x;
            }
        }

        if(spacemode == false)
        {
            horizontalvelocity += Input.GetAxisRaw("Horizontal") * speed;
        }
        else
        {
            if (CheckGround())
            {
                horizontalvelocity += Input.GetAxisRaw("Horizontal") * speed;
            }
        }

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            horizontalvelocity *= Mathf.Pow(0.5f, Time.deltaTime * dampening);
        }



        if (CheckWallRight() == true && Input.GetAxisRaw("Horizontal") == 1 || CheckWallLeft() == true && Input.GetAxisRaw("Horizontal") == -1)
        {
            float normalvelocity = rb.velocity.x;
            rb.velocity = new Vector2(normalvelocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalvelocity, rb.velocity.y);
        }

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

    public bool CheckWallRight()
    {
        Vector2 Pos = (Vector2)transform.position + new Vector2(wallrange, 0);
        Vector2 Scale = (Vector2)transform.localScale + new Vector2(0, wallrange);
        bool NextToWall = Physics2D.OverlapBox(Pos, Scale, 0, CarWalls);
        return NextToWall;
    }

    public bool CheckWallLeft()
    {
        bool NextToWall = false;
        if (CheckWallRight() == false)
        {
            Vector2 Pos = (Vector2)transform.position + new Vector2(-wallrange, 0);
            Vector2 Scale = (Vector2)transform.localScale + new Vector2(0, wallrange);
            NextToWall = Physics2D.OverlapBox(Pos, Scale, 0, CarWalls);
        }
        return NextToWall;
    }
    void OnDrawGizmos()
    {
        Vector2 Pos = (Vector2)transform.position + new Vector2(0, -jumprange);
        Vector2 Scale = (Vector2)transform.localScale + new Vector2(jumprange, 0);
        Vector2 Po2s = (Vector2)transform.position + new Vector2(wallrange, 0);
        Vector2 Scale2 = (Vector2)transform.localScale + new Vector2(0, wallrange);
        Vector2 Pos3 = (Vector2)transform.position + new Vector2(-wallrange, 0);
        Vector2 Scale3 = (Vector2)transform.localScale + new Vector2(0, wallrange);
        Gizmos.color = Color.red;
        if (Input.GetAxisRaw("Vertical") != 0)
            Gizmos.DrawWireCube(Pos, Scale);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Po2s, Scale2);
        Gizmos.DrawWireCube(Pos3, Scale3);
    }
}

