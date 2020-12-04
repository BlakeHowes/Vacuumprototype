using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public float range;
    public GameObject CarRoot;
    public LayerMask Car;
    public TurretBullet bullet;
    public Transform firePoint;
    public float BulletsPerSecond;
    private float coolDownTimer;

    private void OnEnable()
    {
        GetComponent<RotateTarget>().target = GameObject.Find("CarRooot");
        CarRoot = GameObject.Find("CarRooot");
    }

    void ShootGun()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= BulletsPerSecond)
        {
            bool Detected = Physics2D.OverlapCircle(gameObject.transform.position, range, Car);
            if (Detected == true)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                coolDownTimer = 0;
                CarRoot.GetComponent<Moto>().RemoveHealth();
            }
        }
    }

    void Update()
    {
        ShootGun();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}
