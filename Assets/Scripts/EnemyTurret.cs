using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public bool Detected;
    public bool isFiring = false;
    public float range;
    public GameObject CarRoot;
    public LayerMask Car;
    public TurretBullet bullet;
    public Transform firePoint;
    public float setFireRate;
    public float coolDownTime;

    void ShootGun()
    {
        if (coolDownTime <= 0)
        {
            isFiring = true;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            coolDownTime = setFireRate;
        }
        else
        {
            coolDownTime -= Time.deltaTime;
        }
    }

    void Update()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, range, Car);

        if(Detected)
        {
            ShootGun();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
