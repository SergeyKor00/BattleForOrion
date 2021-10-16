using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2 : MonoBehaviour
{


    public Text missionText, shopTask;


    public GameObject missionPanel;

    public GameObject endPanel;

    public Text finalText;

    public Squadron[] shipList;

    private int index, defeatedCount;


    public int waitTime;

    // Start is called before the first frame update
    void Start()
    {
        missionPanel.SetActive(true);
        missionText.text = "Противник перешел в наcтупление. Не дайте ему уничтожить ваш космопорт, защищайте его любой ценой!";
        shopTask.text = "Удерживайте оборону";

        StartCoroutine(createShips());

    }

    public void Loosing()
    {
        endPanel.SetActive(true);
        finalText.text = "Задание провалено";
        Time.timeScale = 0.0f;
    }

    public void Victory()
    {
        defeatedCount++;
        if(defeatedCount == shipList.Length)
        {
            int newValue = PlayerPrefs.GetInt("mission");
            PlayerPrefs.SetInt("mission", (newValue > 2) ? newValue : 3);
            endPanel.SetActive(true);
            finalText.text = "Вы победили";
            Time.timeScale = 0.0f;
        }
    }


    private IEnumerator createShips()
    {
        yield return new WaitForSeconds(5.0f);
        while (index < shipList.Length)
        {
            shipList[index].gameObject.SetActive(true);
            index++;
            yield return new WaitForSeconds(waitTime);

            //shipList[index].gameObject.SetActive(true);
            
        }
    }
}
