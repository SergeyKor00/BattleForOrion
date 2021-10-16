using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : SceneObject
{

    protected int colliderCount;


    protected bool IsPlaced;


    public MeshRenderer myRenderer;

    protected Material defMaterial;

    public Material red, green;

    public bool InScene;

    public int myCost;

    protected new void Start()
    {
        base.Start();
        if (InScene)
        {
            IsPlaced = true;
            Activation();
        }
    }


    public virtual bool readyToPlace()
    {
        return colliderCount == 0;
    }


    public virtual void StartBuilding()
    {
        IsPlaced = true;
        
        StartCoroutine(Building());
    }

    protected void SetColor(bool truePlace)
    {
        if (truePlace)
            myRenderer.material = green;
        else
            myRenderer.material = red;
    }

    private IEnumerator Building()
    {
        yield return new WaitForSeconds(5.0f);
       
        Activation();
    }

    protected virtual void Activation()
    {

    }
}
