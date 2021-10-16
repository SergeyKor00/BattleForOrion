using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour
{

    private Building build;

   

    public void SetBuild(Building b)
    {
        build = b;
    }

    public void Create(GameObject prefab)
    {
        build.StartPlacing(prefab);
    }
}
