using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationEvent : MonoBehaviour
{

    public Mission2 plot;

    private void OnDestroy()
    {
        plot.Loosing();
    }
}
