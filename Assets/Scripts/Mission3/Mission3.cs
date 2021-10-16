using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission3 : MonoBehaviour
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
        missionText.text = "Корабли готовы к бою, флот противника прямо по курсу. Из всех орудий ОГОНЬ! ";
        shopTask.text = "Разбейте флот противника";

        StartCoroutine(checkFlot());

    }

    private IEnumerator checkFlot()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            try
            {
                if (UnitsEnum.FirstCreated[Players.human] == null)
                {
                    Loosing();
                    yield break;
                }
            }
            catch(KeyNotFoundException)
            {
                Loosing();
                yield break;
            }
           

            yield return new WaitForSeconds(2.0f);
        }
    }
    public void Loosing()
    {
        endPanel.SetActive(true);
        finalText.text = "Задание провалено";
        Time.timeScale = 0.0f;
    }

    public void Victory()
    {

        endPanel.SetActive(true);
        finalText.text = "Вы победили";
        Time.timeScale = 0.0f;

    }


  
}
