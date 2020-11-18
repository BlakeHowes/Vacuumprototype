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
    public BoxCollider2D Intake;
    void FixedUpdate()
    {
        //Motor
        JointMotor2D motor = wheel1.motor;
        motor.motorSpeed = CurrentSpeed;

        currentFuel -= Time.deltaTime * consumptionRate;

        if (currentFuel <= 0)
        {
            CurrentSpeed = 0;
        }
        else
        {
            CurrentSpeed = SpeedSetting;
        }

        if (currentFuel < 0)
        {
            currentFuel = 0;
        }

        wheel1.motor = motor;

        //intake
        if (currentFuel > 1500)
        {
            Intake.enabled = false;
        }
        else
        {
            Intake.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "VacuumObject")
        {
            currentFuel += 10;
            Destroy(collision.gameObject);
        }
    }

    public void Off()
    {
        SpeedSetting = 0;
        consumptionRate = 0;
    }

    public void LowGear()
    {
        SpeedSetting = 10;
        consumptionRate = 1;
    }

    public void HighGear()
    {
        SpeedSetting = 40;
        consumptionRate = 3;
    }
}

