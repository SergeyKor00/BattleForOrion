using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private PlayerController player;


    private List<SceneObject> selectedObj;

    public UnitPanel unitPanel;

    public MultyGroup multyCroup;

    public ShipPlane shipPlane;

    public GameObject buildPlane;

    public Text resurses;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        selectedObj = player.selectedObj;
        multyCroup.controller = player;
        multyCroup.SetIcons();

        buildPlane.GetComponent<BuildPanel>().SetBuild(GetComponent<Building>());
    }

    public void SetToPanel(SceneObject obj)
    {
        if(obj is MainStation)
        {
            shipPlane.OpenPanel(obj as MainStation);
            buildPlane.gameObject.SetActive(true);
        }
        else
        {
            shipPlane.gameObject.SetActive(false);
            buildPlane.gameObject.SetActive(false);
        }
        multyCroup.Close();
        unitPanel.SetUnit(obj);
        unitPanel.gameObject.SetActive(true);
    }

    public void SetMultyGroup()
    {
        selectedObj.Sort(selectedObj[0]);
        
        multyCroup.SetGroup(selectedObj);
        unitPanel.gameObject.SetActive(false);
    }

    public void Clear()
    {
        unitPanel.gameObject.SetActive(false);
        multyCroup.gameObject.SetActive(false);
        buildPlane.gameObject.SetActive(false);
        shipPlane.gameObject.SetActive(false);
    }

    private void Update()
    {
        resurses.text = "Ресурсы: " + player.resurses.ToString();
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(UnitsEnum.FirstCreated.Count);
        }
    }
}
