using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    public GameObject bulletPrefab;

    private List<Bullet> myRockets = new List<Bullet>();

    public Players myType;

    private Transform m_transform;

    public float attackDistance;

    private bool IsAttack;

    public int power;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;

        for (int i = 0; i < 5; i++)
        {
            var rock = Instantiate(bulletPrefab, m_transform.position, m_transform.rotation);
            rock.transform.SetParent(m_transform);
            myRockets.Add(rock.GetComponent<Bullet>());
        }

        StartCoroutine(Seaching());

    }

    private void Update()
    {
        if (IsAttack)
        {
            try
            {
               if(Vector3.SqrMagnitude(target.position - m_transform.position) > attackDistance * attackDistance)
               {
                    StopAllCoroutines();
                    StartCoroutine(Seaching());
                    IsAttack = false;
               }
            }
            catch (System.Exception)
            {
                StopAllCoroutines();
                StartCoroutine(Seaching());
                IsAttack = false;
            }
        }
    }


    public void SetTarget(Transform t)
    {
        
        m_transform = transform;
        StopAllCoroutines();
        target = t;
        StartCoroutine(Attack());
    }

    private IEnumerator Seaching()
    {
        yield return new WaitForSeconds(0.2f);
        myType = m_transform.parent.GetComponent<SceneObject>().myType;
        while (true)
        {
            foreach (var list in UnitsEnum.FirstCreated)
            {
                if (list.Key != myType)
                {
                    foreach (UnitsEnum e in list.Value)
                    {
                        var t = e.transform;
                        if (Vector3.SqrMagnitude(t.position - m_transform.position) < attackDistance * attackDistance)
                        {

                            SetTarget(t);
                            yield break;

                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Attack()
    {
        var wait = new WaitForSeconds(0.3f);
        IsAttack = true;
        while (true)
        {
            foreach (var r in myRockets)
            {
                yield return wait;
                r.SetTarget(target, power);
            }
            yield return new WaitForSeconds(1.0f);
        }
        
       
    }


}
