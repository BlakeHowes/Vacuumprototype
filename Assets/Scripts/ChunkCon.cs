﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCon : MonoBehaviour
{
    private FixedJoint2D joint;
    public GameObject EnemyCar;
    private bool removedcheck = false;
    private void OnEnable()
    {
        joint = GetComponent<FixedJoint2D>();
    }

    public void RemoveChunk()
    {
        if (EnemyCar != null)
        {
            if(removedcheck == false)
            {
                gameObject.transform.parent = null;
                if (EnemyCar.GetComponent<EnemyCar>().HealthObjects.Contains(gameObject))
                {
                    EnemyCar.GetComponent<EnemyCar>().RemoveHealthObject(gameObject);
                }
                var rb2d = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                gameObject.layer = 8;
            }
        }
    }

    public void OnDestroy()
    {
        if(EnemyCar != null)
        {
            if (EnemyCar.GetComponent<EnemyCar>().HealthObjects.Contains(gameObject))
            {
                EnemyCar.GetComponent<EnemyCar>().RemoveHealthObject(gameObject);
            }
        }
    }

    public void GetOutOfCar()
    {
        if (EnemyCar != null)
        {
            gameObject.transform.parent = null;
            if (EnemyCar.GetComponent<EnemyCar>().HealthObjects.Contains(gameObject))
            {
                EnemyCar.GetComponent<EnemyCar>().RemoveHealthObject(gameObject);
            }
            gameObject.layer = 8;
        }
    }
}
