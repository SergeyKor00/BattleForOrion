using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mission1 : MonoBehaviour
{

    public Text missionText, shopTask;


    public GameObject missionPanel;

    public GameObject endPanel;

    public Text finalText;

    
    // Start is called before the first frame update
    void Start()
    {
       // Victory();
    }

    public void NextTask(string mission, string task)
    {

        missionPanel.SetActive(true);
        missionText.text = mission;
        shopTask.text = task;
    }

    public void Loosing()
    {
        endPanel.SetActive(true);
        finalText.text = "Задание провалено";
        Time.timeScale = 0.0f;
    }

    public void Victory()
    {
        int newValue = PlayerPrefs.GetInt("mission");
        PlayerPrefs.SetInt("mission", (newValue > 1) ? newValue : 2);
        endPanel.SetActive(true);
        finalText.text = "Вы победили";
        Time.timeScale = 0.0f;
    }
}
