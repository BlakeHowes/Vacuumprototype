using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    Vector3 GunPos;

    private Rigidbody2D ammo;
    public float angle;
    public float fireRate;
    public Transform target;
    public Sprite bullet;
    public Transform firePoint;

    void Awake()
    {
        ammo = GetComponent<Rigidbody2D>();
    }

    public void ShootGun()
    {
        Instantiate(bullet);
    }

    void FixedUpdate()
    {     
        //Vector2 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector2.up);

        //angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
