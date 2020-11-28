using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float setFireRate;
    public float currentFireRate;
    public LayerMask Player;
    public GameObject bullet;
    public Transform firePoint;
    public bool Detected;
    public bool isFiring = false;
    public float range;

    void Awake()
    {
        
    }

    public void ShootGun()
    {
        if(currentFireRate <= 0)
        {
            isFiring = true;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        else
        {
            currentFireRate = 0;
        }
       
    }

    void FixedUpdate()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, range, Player);

        if (Detected)
        {
            ShootGun();
        }

        if(isFiring == true)
        {
            currentFireRate -= Time.deltaTime * setFireRate;
        }

        if(currentFireRate <= 0)
        {
            currentFireRate = setFireRate;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
