using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    public bool inCar;
    public float speed;
    public float health;
    public LayerMask Player;
    public Transform player;

    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (inCar == false)
        {
            enemyRB.AddForce(player.transform.position - transform.position);
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, player.position.y), speed * Time.deltaTime);
        //Vector2 direction = player.transform.position - transform.position * speed * Time.deltaTime;
        Debug.Log("direction");
    }
}
