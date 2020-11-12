using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D rb;
    public float boost;
    public float thrust;
    public bool heatseeking = true;
    public CircleCollider2D explosioncol;
    public PointEffector2D exposioneff;
    public GameObject Player;
    private bool destroy = false;
    public float destroydelay;
    void OnEnable()
    {
        explosioncol.enabled = false;
        exposioneff.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.TransformDirection(Vector3.up)*boost,ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.TransformDirection(Vector3.up) * thrust);
        if(Player != null && heatseeking == true)
        {
            Vector3 direction = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
    }

    private void Update()
    {
        if(destroy == true)
        {
            destroydelay += Time.deltaTime;
        }
        if(destroydelay > 0.1)
        {
            Destroy(gameObject);
        }
    }

    public void TurnOffSeeking()
    {
        heatseeking = false;
    }

    public void AddPlayer(GameObject player)
    {
        Player = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        destroy = true;
    }
    void Explode()
    {
        explosioncol.enabled = true;
        exposioneff.enabled = true;
    }
}
