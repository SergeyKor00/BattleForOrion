using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public GameObject panel;

    public string MenuScene = "MainMenu";

    public GameObject loading;

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Continue()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        loading.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
        loading.SetActive(true);
    }
}
