using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerController
{
    private MainStation station;

    public Transform enemyStation;

    public float maxDistance;

    private int index;

    private int CreatedCount;

    public Mission1 main;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        station = GetComponent<MainStation>();
        index = Random.Range(0, 3);
        CreatedCount = 0;
        timer = 0.0f;
        StartCoroutine(checkStation());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(resurses > station.projects[index].cost)
        {
            station.CreateShip(index);
            index = Random.Range(0, 3);
            CreatedCount++;

        }

        timer += Time.deltaTime;

        if(timer > 120.0f && CreatedCount > 10)
        {
            timer = 0.0f;
            CreatedCount = 0;

           
            foreach(var e in selectedObj)
            {
                e.RightClick(enemyStation, true, true);
            }
            selectedObj.Clear();
        }

    }


    
    public override void SetGroup()
    {
       // base.SetGroup();
    }

    public override void SetSingleObj(SceneObject obj)
    {
        if (obj.myType == Players.human)
            return;
        Vector3 startPos = pointObject.position;
        pointObject.position += new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f) )* maxDistance;
        obj.RightClick(pointObject, true);

        pointObject.position = startPos;

        selectedObj.Add(obj);
    }

    private void OnDestroy()
    {
        main.Victory();
    }

    private IEnumerator checkStation()
    {
        while (true)
        {
            int prev = station.Health;
            yield return new WaitForSeconds(1.0f);
            if(station.Health != prev)
            {
                float dist = 10000.0f;
                UnitsEnum target = null;
                foreach(UnitsEnum e in UnitsEnum.FirstCreated[Players.human])
                {
                    float newDistance = Vector3.SqrMagnitude(e.transform.position - transform.position);
                    if (newDistance < dist)
                    {
                        dist = newDistance;
                        target = e;
                    }
                }

                if(target != null)
                {
                    foreach(var obj in selectedObj)
                    {
                        obj.RightClick(target.transform, false);
                    }
                }
            }
        }
    }
}
