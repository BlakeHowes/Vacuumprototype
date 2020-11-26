using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarExplosion : MonoBehaviour
{
    public Effector2D effector;
    private float timer;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public ParticleSystem particle3;
    private void OnEnable()
    {
        effector.enabled = true;
        particle1.Play(true);
        particle2.Play(true);
        particle3.Play(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            effector.enabled = false;
        }
    }
}
