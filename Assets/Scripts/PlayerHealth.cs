using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float setHealth;
    public float currentHealth;
    
    PlayerUI playerUI;

    void Start()
    {
        playerUI = GetComponent<PlayerUI>();

        currentHealth = setHealth;
    }

    void FixedUpdate()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            HurtPlayer(20);
        }
    }

    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        playerUI.playerHealth.text = currentHealth.ToString();
    }
}
