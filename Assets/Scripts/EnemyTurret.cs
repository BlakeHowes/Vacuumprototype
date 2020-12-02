using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public bool Detected;
    public float radius;
    public GameObject Car;
    public LayerMask Player;
    public TurretBullet bullet;
    public Transform firePoint;
    public float fireRate;
    public float fireRateTimer;

    void FixedUpdate()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, radius, Player);
    }

    void Update()
    {
        if (Detected && fireRate <= 0)
        {
            TurretBullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as TurretBullet;
            fireRateTimer = fireRate;
            fireRateTimer -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TurretBullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as TurretBullet;
            fireRateTimer = fireRate;
            fireRateTimer -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
