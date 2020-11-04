using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpheight;
    public float jumprange;
    public LayerMask layer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            if (CheckifGrounded() == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpheight);

            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), jumprange, layer))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.red);
                Debug.Log("Didn't Hit");
            }
        }
    }

    private bool CheckifGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        bool grounded = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.up), jumprange, mask);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.up) * hit.distance, Color.red);
            grounded = true;
            Debug.Log(hit.point);
        }
        return grounded;
    }
}
