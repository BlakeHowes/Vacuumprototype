using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class DayNight : MonoBehaviour
{
    public Light2D global;
    public float Max;
    public float Min;
    public float speed;
    private bool direction = true;
    private void Start()
    {
        global.intensity = Min;
    }
    void Update()
    {
        float intensity = global.intensity;
        if (intensity >= Max)  {
            direction = false;   }
        if (intensity <= Min)  {
            direction = true;  }
        if(direction == true)
            global.intensity += Time.deltaTime * speed;
        if (direction == false)
            global.intensity -= Time.deltaTime * speed;
    }
}
