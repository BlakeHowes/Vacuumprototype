using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float setFireRate;
    public float coolDownTime;
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
        if(coolDownTime <= 0)  
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
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, range, Player);

        if (Detected)
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
