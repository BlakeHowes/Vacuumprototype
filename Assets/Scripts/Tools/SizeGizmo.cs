using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeGizmo : MonoBehaviour
{
    public float Radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
