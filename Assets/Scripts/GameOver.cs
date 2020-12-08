using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject carRoot;

    void Update()
    {
        if (carRoot == null)
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(2);
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LevelBuildJet");
    }

}
