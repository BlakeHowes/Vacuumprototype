using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float setHealth;

    void Start()
    {
        currentHealth = setHealth;
    }

    void FixedUpdate()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
    }
}
