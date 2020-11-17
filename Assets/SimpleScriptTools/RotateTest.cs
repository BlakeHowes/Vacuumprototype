using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public float rot1;
    public float rot2;
    public float rot3;
    public float speed;
    public float speed2;
    void FixedUpdate()
    {
        rot1 += Time.deltaTime * speed;
        rot2 += Time.deltaTime * speed2;
        Quaternion rot = Quaternion.Euler(rot1, rot2, rot3);
        transform.rotation = rot;
    }
}
