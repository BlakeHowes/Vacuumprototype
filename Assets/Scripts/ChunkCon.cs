using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCon : MonoBehaviour
{
    private FixedJoint2D joint;
    public GameObject EnemyCar;

    private void OnEnable()
    {
        joint = GetComponent<FixedJoint2D>();
    }
    void Update()
    {
        if(EnemyCar != null)
        {
            if (joint.enabled == false)
            {
                gameObject.transform.parent = null;
                EnemyCar.GetComponent<EnemyCar>().RemoveHealthObject(gameObject);
            }
        }
    }
}
