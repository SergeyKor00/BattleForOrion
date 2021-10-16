using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningShip : MonoBehaviour
{
    public LEarning plot;

    public int index, secondIndex;


    

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            plot.Next(secondIndex);
        }
    }

    public void OnMouseDown()
    {
        plot.Next(index);
    }
}
