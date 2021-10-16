using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desrtoyer : GameUnit
{

    public float pauseDistance;

    private float agentSpeed;

    public Transform firstCannon, secondCannon;

    private bool IsFirst;

    public GameObject lazerPrefab;

    private bool ReadyToFire;


    public List<Turret> myTurrets;


    private void Awake()
    {
        Debug.Log("Turrets");
        foreach (var t in myTurrets)
        {
            t.myType = myType;
        }
    }
    // Start is called before the first frame update
    new void Start()
    {
        agentSpeed = agent.speed;
        base.Start(); 

        
    }


    new void Update()
    {
        if (!IsAttacking)
        {
            base.Update();

            if (Vector3.SqrMagnitude(agent.nextPosition - m_transform.position) > pauseDistance)
            {
                agent.speed = 0.0f;
            }
            else
                agent.speed = agentSpeed;
        }
        else
        {
            try
            {
                UpdatePosition();
            }
            catch (System.Exception)
            {
                IsAttacking = false;
                StopAllCoroutines();
                
                StartCoroutine(seaching());
            }
        }
        
    }

    private void UpdatePosition()
    {
        if (Vector3.SqrMagnitude(target.position - m_transform.position) > Mathf.Pow(attackDistance / 2, 2))
        {
            m_transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }

        var look = Quaternion.LookRotation(target.position - m_transform.position);
        if (Quaternion.Angle(m_transform.rotation, look) > 10.0f)
        {
            ReadyToFire = false;
            m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, look, AngleSpeed * Time.deltaTime);
            m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            ReadyToFire = true;
        }

    }


    private IEnumerator attack()
    {
        while (true)
        {

           
            var point = IsFirst ? firstCannon : secondCannon;
            IsFirst = !IsFirst;

            if (ReadyToFire){
                var bullet = Instantiate(lazerPrefab, point.position, point.rotation);
                bullet.GetComponent<HeavyBullet>().SetTarget(target, 10, power);
            }
            yield return new WaitForSeconds(0.3f);

        }
    }

    public override bool Attack()
    {


        foreach(var t in myTurrets)
        {
            t.SetTarget(target);

        }
        if (target.GetComponent<Fighter>() != null)
            return false;

        StartCoroutine(attack());
        speed = StartSpeed;
        IsAttacking = true;
        ReadyToFire = false;
        return true;
    }


}
