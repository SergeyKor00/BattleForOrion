using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningStation : Station
{

    private bool miningPlace;

    [SerializeField]
    private AsteroidField field;

    private int  droneValue;

    private Transform closePos;


    public GameObject dronePrefab;

    // Start is called before the first frame update
    new void Start()
    {
        defMaterial = myRenderer.material;
        base.Start();
       
        if(!InScene)
            SetColor(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlaced)
            return;
        if (other.gameObject.CompareTag("Asteroids"))
        {
            var f = other.GetComponent<AsteroidField>();
            if(!f.FieldFull)
            {
                field = f;
                
                miningPlace = true;
            }
            
        }
        else
        {
            colliderCount++;
        }
        SetColor(readyToPlace());
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlaced)
            return;
        if (other.gameObject.CompareTag("Asteroids"))
        {
            if (other.GetComponent<AsteroidField>() == field)
            {
                field = null;
                miningPlace = false;
            }
                
        }
        else
        {
            colliderCount--;

        }
        SetColor(readyToPlace());
    }

    public override bool readyToPlace()
    {
        return miningPlace && base.readyToPlace();
    }

    public override void StartBuilding()
    {
        field.miningCount++;
        base.StartBuilding();
    }

    protected override void Activation()
    {
        StartCoroutine(mineing());

        myRenderer.material = defMaterial;
       
    }


    private IEnumerator mineing()
    {
        var Wait = new WaitForSeconds(5.0f);
        while (true)
        {

            controller.resurses += 10;
            yield return Wait;

        }
    }


    public override void DoAction(string info)
    {
        if(info == "drone")
        {


        }
    }

    private void CreateDrone()
    {
        var drone = Instantiate(dronePrefab, transform.position, Quaternion.identity).GetComponent<Drone>();
        drone.SetPoint(closePos, transform);
        droneValue++;
    }


    private void OnDestroy()
    {
        field.miningCount--;
    }
}
