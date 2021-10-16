using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultyGroup : MonoBehaviour
{
    private List<Icon> icons = new List<Icon>();

    public Transform plane;

    public PlayerController controller;

    private bool IsActive;

   
    public void SetIcons()
    {
        foreach (var b in plane.GetComponentsInChildren<Icon>(true))
        {
            icons.Add(b);
        }
    }

    private void ClearList()
    {
        foreach (var b in icons)
        {
            if (b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(false);
            }
            else
                break;
        }
    }


    public void SetGroup(List<SceneObject> objects)
    {
        IsActive = true;
        gameObject.SetActive(true);
        ClearList();

        int i = 0;
        foreach(var obj in objects)
        {
            icons[i].gameObject.SetActive(true);
            icons[i].SetListener(obj, controller);
            i++;
            if(i == icons.Count)
            {
                var newIcon = Instantiate(icons[i].gameObject, plane);
                icons.Add(newIcon.GetComponent<Icon>());
            }
        }
    }

    public void Close()
    {
        if (!IsActive)
            return;
        ClearList();
        IsActive = false;
        gameObject.SetActive(false);

    }
}
