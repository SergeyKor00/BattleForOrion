using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningTarget : SceneObject
{
    public int index;

    public LEarning plot;

    private void OnDestroy()
    {
        plot.Next(index);
    }

}
