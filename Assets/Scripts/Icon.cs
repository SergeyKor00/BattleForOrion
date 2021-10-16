using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    private SceneObject myObject;
    private PlayerController controller;

    private Image my_image;

    

    protected void Start()
    {
        GetComponent<Button>().onClick.AddListener(Action);
        
    }
    public void SetListener(SceneObject obj, PlayerController c)
    {
        myObject = obj;
        controller = c;
        GetComponent<Image>().sprite = obj.myIcon;
        
    }

    public void Action()
    {
        controller.SetSingleObj(myObject);
    }

    private void Update()
    {
        if(myObject == null)
        {
            gameObject.SetActive(false);
        }
    }

}
