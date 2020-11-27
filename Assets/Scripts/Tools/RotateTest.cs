using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    private float rot1;
    private float rot2;
    private float rot3;
    public float Rotationx;
    public float Rotationy;
    public float Rotationz;
    void FixedUpdate()
    {
        rot1 += Time.deltaTime * Rotationx;
        rot2 += Time.deltaTime * Rotationy;
        rot3 += Time.deltaTime * Rotationz;
        Quaternion rot = Quaternion.Euler(rot1, rot2, rot3);
        transform.rotation = rot;
    }
}
