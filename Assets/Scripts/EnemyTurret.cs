using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public int damage;
    public bool Detected;
    public float radius;
    public GameObject Car;
    public LayerMask Player;
    public GameObject bullet;

    void FixedUpdate()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, radius, Player);
    }

    void ShootTurret()
    {
        if(Detected)
        {
            Instantiate(bullet);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
