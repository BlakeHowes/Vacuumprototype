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
    [SerializeField]
    private List<GameObject> HealthObjects = new List<GameObject>();
    private float timer;
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

        if(timer > Random.Range(10,15))
        {
            SpeedMatchScaleTemp = Random.Range(SpeedMatchScale - 12, SpeedMatchScale + 15);
            timer = 0f;
        }

        if(HealthObjects.Count == 0)
        {
            DestroyCar();
        }

        foreach(GameObject obj in HealthObjects)
        {
            if(obj == null)
            {
                HealthObjects.Remove(obj);
            }
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

    private void DestroyCar()
    {
        Destroy(gameObject);
    }

    public void RemoveHealthObject(GameObject obj)
    {
        if (HealthObjects.Contains(obj))
        {
            HealthObjects.Remove(obj);
        }
    }
}
