using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour
{
    public float range;
    public LayerMask playermask;
    float checktplayertimer;
    public GameObject Rocket;
    public GameObject Player;
    public Collider2D col;
    public float coltimer;
    public bool colbool = false;

    void Update()
    {
        checktplayertimer += Time.deltaTime;
        if(checktplayertimer > 1)
        {
            checktplayertimer = 0f;

            if(playercheck() == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.right), range);
                col.enabled = false;
                colbool = true;
                Vector3 direction = Player.transform.position - transform.position;
                Quaternion zrotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
                var Rocket2 = Instantiate(Rocket, transform.position, zrotation);
                Rocket.GetComponent<Rocket>().AddPlayer(Player);
            }
        }
        if(colbool == true)
        {
            coltimer += Time.deltaTime;
        }
        if(coltimer > 1)
        {
            col.enabled = true;
            coltimer = 0f;
        }
    }

    private bool playercheck()
    {
        bool isplayerinrange = false;
        if(Physics2D.OverlapCircle(transform.position, range, playermask))
        {
            isplayerinrange = true;
        }
        return isplayerinrange;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
