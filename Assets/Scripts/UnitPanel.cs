using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnitPanel : MonoBehaviour
{
    public Slider healthSlider;

    private SceneObject currObject;

    public Text unitName, unitHealth;

    public Sprite[] icons;

    public Image iconImage;


    private bool IsClosed;

    
    // Start is called before the first frame updat

    private void Update()
    {
        //try
        //{
        if (currObject == null)
        {
            gameObject.SetActive(false);
            return;
        }


        healthSlider.value = currObject.Health;
        unitHealth.text = currObject.Health.ToString() + "/" + currObject.StartHealth.ToString();

        
        //}
        //catch (System.Exception)
        //{
        //    Debug.Log("Stop");
        //    gameObject.SetActive(false);
        //}
    }

    public void SetUnit(SceneObject obj)
    {
        gameObject.SetActive(true);
        currObject = obj;
        healthSlider.maxValue = obj.StartHealth;
        unitName.text = obj.myName;
        iconImage.sprite = obj.myIcon;
       
    }



}
