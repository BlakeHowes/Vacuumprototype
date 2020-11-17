using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTarget : MonoBehaviour
{
    public GameObject target;
    void FixedUpdate()
    {
        transform.right = target.transform.position - transform.position;
    }
}
