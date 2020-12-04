using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Moto : MonoBehaviour
{
    public HingeJoint2D wheel1;
    private float CurrentSpeed;
    public float SpeedSetting;
    public float currentFuel;
    public float consumptionRate;
    public BoxCollider2D Intake;
    public int health = 100;
    public ParticleSystem sparks;
    public ParticleSystem sparks2;
    public ParticleSystem blood;
    public Text healthtext;
    PlayerUI playerUI;
    public GameObject Tutorial;
    private void Awake()
    {
        sparks.Pause();
        sparks2.Pause();
        blood.Pause();
    }

    public void Update()
    {
        if(healthtext != null)
        {
            healthtext.text = health.ToString();
        }
    }

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

        if(health <= 0)
        {
            if(healthtext != null)
            {
                healthtext.text = ("0");
            }
            Destroy(gameObject);
        }
    }

    public void AddHealth()
    {
        health += 5;
        if(health > 100)
        {
            health = 100;
        }
    }

    public void RemoveHealth()
    {
        health--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != null)
        {
            if (collision.gameObject.layer == LayerMask.GetMask("Chunk"))
            {
                collision.gameObject.TryGetComponent<ChunkCon>(out ChunkCon con);
                if (con != null)
                {
                    con.RemoveChunk();
                }
            }

            if (collision.gameObject.tag == "VacuumObject")
            {
                if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyscript))
                {
                    if (enemyscript != null)
                    {
                        blood.Play();
                        Debug.Log("Blood");
                    }
                }
                else
                {
                    sparks.Play();
                    sparks2.Play();
                }
                currentFuel += 10;
                AddHealth();
                Destroy(collision.gameObject);
            }
        }
    }

    public void Off()
    {
        SpeedSetting = 0;
        consumptionRate = 0;
        if(Tutorial.activeSelf == true)
        {
            Tutorial.SetActive(false);
        }
    }

    public void LowGear()
    {
        SpeedSetting = 10;
        consumptionRate = 1;
        if (Tutorial.activeSelf == true)
        {
            Tutorial.SetActive(false);
        }
    }

    public void HighGear()
    {
        SpeedSetting = 40;
        consumptionRate = 3;
        if (Tutorial.activeSelf == true)
        {
            Tutorial.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthtext.text = health.ToString();
    }
}

