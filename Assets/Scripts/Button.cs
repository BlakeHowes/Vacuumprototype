using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool off;
    public bool Low;
    public bool high;
    public GameObject CarRoot;
public void Press()
    {
        if(off == true)
        {
            CarRoot.gameObject.GetComponent<Moto>().Off();
        }
        if (Low == true)
        {
            CarRoot.gameObject.GetComponent<Moto>().LowGear();
        }
        if (high == true)
        {
            CarRoot.gameObject.GetComponent<Moto>().HighGear();
        }
    }
}
