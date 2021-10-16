using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlane : MonoBehaviour
{

    private MainStation spacePort;

    public List<Image> shipQueue;

    public List<Text> shipCost;

    public Slider slider;

    private int buildCount;
    


    // Update is called once per frame
    void Update()
    {
        try
        {
            if(buildCount != spacePort.buildList.Count)
                 UpdatePanel();

            buildCount = spacePort.buildList.Count;
            slider.value = spacePort.Timer;
        }
        catch(System.Exception)
        {

        }
    }

    public void UpdatePanel()
    {
       
            int i = 0;
            foreach(var p in spacePort.buildList)
            {
                shipQueue[i].transform.parent.gameObject.SetActive(true);
                shipQueue[i].sprite = p.prefab.GetComponent<SceneObject>().myIcon;
                i++;
            }
            while(i < shipQueue.Count)
            {
                shipQueue[i].transform.parent.gameObject.SetActive(false);
                i++;
            }
        
        if(spacePort.buildList.Count == 0)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.maxValue = spacePort.buildList[0].time;
        }

    }


    public void OpenPanel(MainStation station)
    {
        gameObject.SetActive(true);
        spacePort = station;
        UpdatePanel();
        if(station.buildList.Count > 0)
            slider.gameObject.SetActive(true);
        buildCount = spacePort.buildList.Count;

        for(int i = 0; i < shipCost.Count; i++)
        {
            shipCost[i].text = station.projects[i].cost.ToString();
        }
    }

    public void AddShip(int number)
    {
        spacePort.CreateShip(number);
    }


    public void RemoveShip(int number)
    {
        spacePort.RemoveShip(number);
    }
}
