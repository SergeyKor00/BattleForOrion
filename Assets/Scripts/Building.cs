using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public Camera cam;

    private bool IsPlacing;

    private Station stationProject;

    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlacing)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000.0f, 1<<9))
            {
                stationProject.transform.position = hit.point;

                if(Input.GetMouseButtonDown(0) && stationProject.readyToPlace())
                {
                    stationProject.StartBuilding();
                    stationProject.GetComponent<UnitsEnum>().Add();
                    IsPlacing = false;

                }
                else if (Input.GetMouseButtonDown(1))
                {
                    IsPlacing = false;
                    Destroy(stationProject.gameObject);
                }
            }



        }
    }

    public void StartPlacing(GameObject prefab)
    {

        int cost = prefab.GetComponent<Station>().myCost;
        if (cost <= controller.resurses)
        {
            controller.resurses -= cost;
            IsPlacing = true;
            stationProject = Instantiate(prefab).GetComponent<Station>();
            stationProject.controller = controller;
        }
        
    }
}
