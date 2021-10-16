using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipProject {
    public GameObject prefab;

    public float time;

    public int cost;

}

public class MainStation : SceneObject
{

    public List<ShipProject> projects;


    public List<ShipProject> buildList = new List<ShipProject>();


    public int level;

    private bool IsBuilding;

    public float Timer{ get; private set; }

    public Transform startPoint;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (IsBuilding)
        {
            Timer += Time.deltaTime;
            if(Timer > buildList[0].time)
            {
                
                var ship = Instantiate(buildList[0].prefab, startPoint.position, startPoint.rotation).GetComponent<SceneObject>();
                ship.controller = controller;

  
                ship.myType = myType;
                ship.GetComponent<GameUnit>().CalculateSpeed();
                var unit = ship.GetComponent<UnitsEnum>();

                unit.myType = myType;
                unit.Add();
                if (myType != Players.human)
                {
                    controller.SetSingleObj(ship);
                }

                buildList.RemoveAt(0);
                Timer = 0.0f;
                if(buildList.Count == 0)
                {
                    IsBuilding = false;
                    
                }
            }
        }
        
    }

    public void CreateShip(int number)
    {
        if(controller.resurses >= projects[number].cost && buildList.Count < 5)
        {
            buildList.Add(projects[number]);
            controller.resurses -= projects[number].cost;
            IsBuilding = true;
        }
        
    }

    
    public void RemoveShip(int number)
    {

        controller.resurses += buildList[number].cost;
        buildList.RemoveAt(number);
        if(number == 0)
            Timer = 0.0f;
        if(buildList.Count == 0)
        {
            IsBuilding = false;
        }
    }
    
}
