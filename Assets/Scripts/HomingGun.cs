using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingGun : MonoBehaviour
{
    public LayerMask Player;
    public float speed;
    public float radius;
    public bool Detected;
    public Rigidbody2D missile;
    public bool MissilePresent = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Detected = Physics2D.OverlapCircle(gameObject.transform.position, radius, Player);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * radius, Color.green);

        transform.Rotate(0, 0, 50 * Time.deltaTime);

        if(Physics2D.Raycast (gameObject.transform.position, Vector2.up, Mathf.Infinity, LayerMask.GetMask ("Player")))
        {
            
        }

        if (MissilePresent == false && Detected)
        {
            StartCoroutine(ShootMissile());
            //Debug.Log("Detected");           
        }
        else
        {
            //Debug.Log("Not Detected");
        }
    }

    IEnumerator ShootMissile()
    {
        if(Detected)
        {
            SpawnMissile();
        }
        else
        {
            Debug.Log("NoSpawnedMissile");
            yield return null;
        }      
    }

    void SpawnMissile()
    {
        if(MissilePresent == false)
        {
            MissilePresent = true;
            Instantiate(missile);
            Debug.Log("SpawnMissile");
        }
        else
        {

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
