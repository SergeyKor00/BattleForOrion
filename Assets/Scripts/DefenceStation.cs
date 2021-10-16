using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStation : Station
{

    private Transform target;

    public GameObject bulletPrefab;


    public Transform rotationPart, first, second;

    public float attackDistance;

    private bool IsAttack;

    public int power;

    private bool isFirst;

    private bool isActive;

    private float timer;

    public float AngleSpeed;

    

    private GameObject redObj, greenObj, realObj;
    // Start is called before the first frame update
    new void Start()
    {
        timer = 0.0f;
        isActive = false;
        //base.Start();
        isFirst = true;
        

        redObj = transform.GetChild(0).gameObject;
        greenObj = transform.GetChild(1).gameObject;
        realObj = transform.GetChild(2).gameObject;
        base.Start();
    }

    private void Update()
    {
        if (IsAttack)
        {
            if(target != null) { 
                UpdateRotation();
            }
            else
            {
                IsAttack = false;
                StartCoroutine(Seaching());
            }
        }
    }

    private void UpdateRotation()
    {
        
        Vector3 point = target.position;
        point.y = rotationPart.position.y;

        var look = Quaternion.LookRotation(point - rotationPart.position);
        rotationPart.rotation = Quaternion.RotateTowards(rotationPart.rotation, look, AngleSpeed * Time.deltaTime);


        timer += Time.deltaTime;
        if(timer >= 0.3f && Quaternion.Angle(look, rotationPart.rotation) < 10.0f)
        {

            if(Vector3.SqrMagnitude(target.position - rotationPart.position) > attackDistance*attackDistance)
            {
                target = null;
                return;
            }
            Transform gun = isFirst ? first : second;
            isFirst = !isFirst;
            Instantiate(bulletPrefab, gun.position, gun.rotation).GetComponent<HeavyBullet>().SetTarget(target, 10.0f, power);
            timer = 0.0f;

            
        }



    }


    public override void RightClick(Transform target, bool IsPoint, bool AutoAttack = false)
    {
        if (!isActive || IsPoint )
            return;

        if(target.GetComponent<SceneObject>().myType != myType)
        {
            IsAttack = true;
            this.target = target;
            StopAllCoroutines();
        }
        


    }

    protected override void Activation()
    {
        isActive = true;
        IsPlaced = true;
        StartCoroutine(Seaching());
        Destroy(greenObj);
        Destroy(redObj);
        realObj.SetActive(true);
    }

    private IEnumerator Seaching()
    {
        Debug.Log("corutine");
        while (true)
        {
            foreach (var list in UnitsEnum.FirstCreated)
            {
                if (list.Key != myType)
                {
                    foreach (UnitsEnum e in list.Value)
                    {
                        var t = e.transform;
                        if (Vector3.SqrMagnitude(t.position - rotationPart.position) < attackDistance * attackDistance)
                        {
                            Debug.Log("NewTarget");
                            IsAttack = true;
                            target = t;
                            yield break;

                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlaced)
        {
            colliderCount++;
            redObj.SetActive(!base.readyToPlace());
            greenObj.SetActive(base.readyToPlace());


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPlaced)
        {
            colliderCount--;
            redObj.SetActive(!base.readyToPlace());
            greenObj.SetActive(base.readyToPlace());


        }
            
    }

   
}
