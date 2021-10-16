using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStation : MonoBehaviour
{
    public FirstPlace mission;

    private void OnDestroy()
    {
        mission.Remove(gameObject);
    }


}
