using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectUnit : MonoBehaviour {
    // компоненет объекта UnitManager, отвечает за рамку выделения и выбор юнитов
    public bool IsSelect;
    private float selX, selY;
    private float Weight, Height;

    private float X_old, Y_old;
    
    private Vector3 StartPoint, endPoint;

    public Texture2D select;

    public List<SceneObject> selectedOj;
    private Ray _ray;
    private RaycastHit hit;

    private SceneObject singleObject;

    private PlayerController controller;


    // Use this for initialization
    void Start () {
        controller = GetComponent<PlayerController>();
        selectedOj = controller.selectedObj;
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {

            // создание стартовой точки выделения
            //IsSelect = true;
            X_old = Input.mousePosition.x;
            Y_old = Input.mousePosition.y;
            _ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(_ray, out hit, 10000.0f))
            {
                IsSelect = true;
                StartPoint = hit.point;
            }
            LayerMask mask = 1 << 9;
            if (Physics.Raycast(_ray, out hit, 10000.0f, ~mask))
            {
                var hitObj = hit.collider.gameObject;
                if (hitObj.CompareTag("SceneObj"))
                {
                    singleObject = hitObj.GetComponent<SceneObject>();
                }
            }

        }
        if (Input.GetMouseButtonUp(0)) 
        {
            
            
            IsSelect = false;
            _ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out hit, 10000.0f))
            {
                
                endPoint = hit.point;
            }
            if(StartPoint != endPoint)
                FindSelect();
            else
            {

                controller.SetSingleObj(singleObject);
                singleObject = null;
                
            }
        }
        if (IsSelect)
        {
            selX = Input.mousePosition.x;
            selY = Screen.height - Input.mousePosition.y;
            Weight = X_old - Input.mousePosition.x;
            Height = Input.mousePosition.y - Y_old;

        }
	}

    private void OnGUI()
    {
        if (IsSelect)
            GUI.DrawTexture(new Rect(selX, selY, Weight, Height), select);
    }

    private void FindSelect()
    {
        UnitsEnum units = UnitsEnum.FirstCreated[Players.human];
        if (units == null) return;


        
        controller.SetSingleObj(singleObject);


        foreach (UnitsEnum e in units)
        {
            if (e.IsStantion)
                continue;

            float x = e.transform.position.x;
            float z = e.transform.position.z;
            if ((x > StartPoint.x && x < endPoint.x) || (x < StartPoint.x && x > endPoint.x))
                if ((z > StartPoint.z && z < endPoint.z) || (z < StartPoint.z && z > endPoint.z))
                {
                    if(singleObject == null)
                        selectedOj.Add(e.GetComponent<SceneObject>());
                    else if(singleObject.gameObject != e.gameObject)
                        selectedOj.Add(e.GetComponent<SceneObject>());
                    //if(e.gameObject != singleObject.gameObject)

                    //e.gameObject.GetComponent<ShowHealth>().MyShowHP.gameObject.SetActive(true);
                }
        }
        if(selectedOj.Count != 0)
        {
            controller.SetGroup();
        }
        
    }

    
}
