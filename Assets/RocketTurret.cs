using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour
{
    public float range;
    public LayerMask playermask;
    float checktplayertimer;

    void Start()
    {
        
    }

    void Update()
    {
        checktplayertimer += Time.deltaTime;
        if(checktplayertimer > 1)
        {
            playercheck();
            checktplayertimer = 0f;
        }
    }

    private bool playercheck()
    {
        bool isplayerinrange = false;
        if(Physics2D.OverlapCircle(transform.position, range, playermask))
        {
            isplayerinrange = true;
            Debug.Log("Inrange");
        }
        return isplayerinrange;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
