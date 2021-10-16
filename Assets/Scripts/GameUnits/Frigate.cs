using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frigate : GameUnit
{


    public Transform Cannon;

    public GameObject lazerPrefab;

    private bool ReadyToFire;

    public List<Turret> myTurrets;


    private void Awake()
    {
        Debug.Log("turrets");
        foreach (var t in myTurrets)
        {
            t.myType = myType;
        }
    }
    // Start is called before the first frame update



    new void Update()
    {
        if (!IsAttacking)
        {
            base.Update();

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
                Debug.Log("Exception");
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

            if (ReadyToFire)
            {
                var bullet = Instantiate(lazerPrefab, Cannon.position, Cannon.rotation);
                bullet.GetComponent<HeavyBullet>().SetTarget(target, 10, power);
            }
            yield return new WaitForSeconds(0.5f);

        }
    }

    public override bool Attack()
    {
        Debug.Log("Attack");
        foreach (var t in myTurrets)
        {
            t.SetTarget(target);

        }
        StartCoroutine(attack());
        speed = StartSpeed;
        IsAttacking = true;
        ReadyToFire = false;
        return true;
    }

}
