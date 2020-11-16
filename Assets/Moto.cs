using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto : MonoBehaviour
{
    public HingeJoint2D wheel1;
    private float CurrentSpeed;
    public float SpeedSetting;
    public float currentFuel;
    public float consumptionRate;

    private void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        JointMotor2D motor = wheel1.motor;
        motor.motorSpeed = CurrentSpeed;


        if (wheel1.jointSpeed != 0)
        {
            currentFuel -= Time.deltaTime;
        }

        if (currentFuel <= 0)
        {
            CurrentSpeed = 0;
        }
        else
        {
            CurrentSpeed = SpeedSetting;
        }

        if (currentFuel > 1500)
        {
            currentFuel = 1500; //turn off intake
        }

        if (currentFuel < 0)
        {
            currentFuel = 0;
        }

        wheel1.motor = motor;
    }

    public void LowGear()
    {
        SpeedSetting = 2;
        consumptionRate = 1;
    }

    public void HighGear()
    {
        SpeedSetting = 5;
        consumptionRate = 3;
    }
}

