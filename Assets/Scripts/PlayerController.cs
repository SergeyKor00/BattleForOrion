using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    

    public List<SceneObject> selectedObj = new List<SceneObject>();

    private UIManager UI;

    public Camera cam;

    public Transform pointObject;

    [Range(0, 100000)]
    public int resurses;

    // Start is called before the first frame update
    void Start()
    {

        
        UI = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            RightClick();
    }

    private void RightClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000.0f))
        {
            var collider = hit.collider.gameObject;
            if (collider.CompareTag("Surface"))
            {
                pointObject.position = hit.point;
                foreach (var e in selectedObj)
                    e.RightClick(pointObject, true);

            }
            else if (collider.CompareTag("SceneObj"))
            {
               
                foreach(var e in selectedObj)
                {
                    e.RightClick(collider.transform, false);
                }

            }

        }
    }

    public virtual void SetSingleObj(SceneObject obj)
    {
        foreach(var ship in selectedObj)
        {
            ship.SetSelection(false);
        }
        selectedObj.Clear();
        if (obj == null)
        {
            UI.Clear();
            return;
        }
    
        if(obj.myType == Players.human)
        {
            selectedObj.Add(obj);
            obj.SetSelection(true);
            UI.SetToPanel(obj);
        }

        
    }

   

    public virtual void SetGroup()
    {
        if(selectedObj.Count == 1)
        {
            SetSingleObj(selectedObj[0]);
        }
        else
        {
            foreach(var ship in selectedObj)
            {
                ship.SetSelection(true);
            }
            UI.SetMultyGroup();
        }
    }
}
