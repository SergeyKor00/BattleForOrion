using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionLearning : MonoBehaviour
{

    public LEarning plot;

    public int index;

    private SelectUnit selection;
    // Start is called before the first frame update
    void Start()
    {
        selection = GetComponent<SelectUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(plot.index == index && selection.IsSelect)
        {
            plot.Next(index);
            enabled = false;
        }
    }
}
