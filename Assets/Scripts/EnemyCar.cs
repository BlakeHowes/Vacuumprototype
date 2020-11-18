using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public GameObject CarRoot;
    [SerializeField]
    private float Range;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float SpeedMatchScale;
    [SerializeField]
    private HingeJoint2D Wheel;
    public float timer;
    private float SpeedMatchScaleTemp;
    void Start()
    {
        SpeedMatchScaleTemp = SpeedMatchScale;
    }

    void Update()
    {
        if(CarRoot.transform.position.x + Range < transform.position.x)
        {
            DriveLeft();
        }
        if (CarRoot.transform.position.x - Range > transform.position.x)
        {
            DriveRight();
        }
        if(transform.position.x > CarRoot.transform.position.x - Range && transform.position.x < CarRoot.transform.position.x + Range)
        {
            JointMotor2D motor = Wheel.motor;
            motor.motorSpeed = CarRoot.GetComponent<Rigidbody2D>().velocity.x * SpeedMatchScaleTemp;

            Wheel.motor = motor;
        }
        timer += Time.deltaTime;

        if(timer > Random.Range(2,10))
        {
            SpeedMatchScaleTemp = Random.Range(SpeedMatchScale - 3, SpeedMatchScale + 3);
            timer = 0f;
        }
    }
    private void DriveLeft()
    {
        JointMotor2D motor = Wheel.motor;
        motor.motorSpeed = -Speed;

        Wheel.motor = motor;
    }
    private void DriveRight()
    {
        JointMotor2D motor = Wheel.motor;
        motor.motorSpeed = Speed;

        Wheel.motor = motor;
    }
}
