using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool off;
    public bool Low;
    public bool high;
    public bool Temple;
    public GameObject CarRoot;
    public float timer;
    public bool BeginReset = false;
    public Color resetcolor;
    public GameObject Statue;
    public GameObject platform;
    private void Start()
    {
        var rend = GetComponent<SpriteRenderer>();
        resetcolor = rend.color;
    }

    void Update()
    {
        if (BeginReset == true)
        {
            timer += Time.deltaTime;
            if (timer > 0.2)
            {
                var rend = GetComponent<SpriteRenderer>();
                rend.color = resetcolor;
                timer = 0f;
                BeginReset = false;
            }
        }
    }
    public void Press()
    {
        if(off == true)
        {
            flash();
            CarRoot.gameObject.GetComponent<Moto>().Off();
        }
        if (Low == true)
        {
            flash();
            CarRoot.gameObject.GetComponent<Moto>().LowGear();
        }
        if (high == true)
        {
            flash();
            CarRoot.gameObject.GetComponent<Moto>().HighGear();
        }
        if(Temple == true)
        {
            flash();
            Destroy(Statue.gameObject);
            Destroy(platform.gameObject);
        }
    }

    private void flash()
    {
        var rend = GetComponent<SpriteRenderer>();
        rend.color = Color.white;
        BeginReset = true;
    }
}
