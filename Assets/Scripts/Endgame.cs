using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    public GameObject CarRoot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Car")
        {           
            StartCoroutine(End());
        }       
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3);
        CarRoot.gameObject.GetComponent<Moto>().Off();
        Debug.Log("Off");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName: "MainMenu");
        Debug.Log("Menu");
    }
}
