using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class SceneObject : MonoBehaviour, IComparer<SceneObject>
{

    public string myName;

    public Players myType
    {
        get { return type; }
        set { type = value;
            if(value == Players.programm1)
            {

                GetComponent<ObjectsGUI>().miniMap.color = Color.red;
            }
        }
    }
    [SerializeField]
    private Players type;

    public PlayerController controller;

    public int StartHealth;

    public Sprite myIcon;

    public GameObject explosion;

    private ObjectsGUI myGUI;


    

    public int Health {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Destruction();
            }
                
        }
    }

    private int health;
    protected void Start()
    {

        myGUI = GetComponent<ObjectsGUI>();

        health = StartHealth;

        if(type == Players.programm1)
        {
            myGUI.miniMap.color = Color.red;
        }
    }

    public virtual void LeftClick()
    {

    }
   
    public virtual void RightClick(Transform target, bool IsPoint, bool AutoAttack = false)
    {

    }

    //private void OnMouseDown()
    //{
        
    //    controller.SetSingleObj(this);
    //}
    public void SetSelection(bool IsActive)
    {
        myGUI.selectRender.gameObject.SetActive(IsActive);
    }

    public virtual void DoAction(string info)
    {

    }

    private void Destruction()
    {
        explosion.transform.SetParent(null);
        explosion.SetActive(true);
        Destroy(gameObject);
        Destroy(explosion, 1.5f);
        controller.selectedObj.Remove(this);
    }

    public int Compare(SceneObject x, SceneObject y)
    {
        return -x.Health.CompareTo(y.Health);
    }
}
