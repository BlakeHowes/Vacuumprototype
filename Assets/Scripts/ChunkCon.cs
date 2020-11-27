using System.Collections;
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
                EnemyCar.GetComponent<EnemyCar>().RemoveHealthObject(gameObject);
                var rb2d = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                gameObject.layer = 8;
            }
        }
    }
}
