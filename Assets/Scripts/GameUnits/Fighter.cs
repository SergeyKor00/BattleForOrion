using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : GameUnit
{

    public List<Transform> rocketPosition;

    private List<Bullet> myRockets = new List<Bullet>();


    public GameObject rocketPrefab;

   

    private Vector3 pos;

    public float reverseDistance, distanceFromTarget;

    private bool attack;


    new void Start()
    {
        base.Start();
        foreach(var t in rocketPosition)
        {
            var rock = Instantiate(rocketPrefab, t.position, t.rotation);
            rock.transform.SetParent(t);
            myRockets.Add(rock.GetComponent<Bullet>());

        }
        attack = false;
        //height = m_transform.position.y;

    }

    new void Update()
    {
        if (!IsAttacking)
        {
            base.Update();
            return;
        }
        if(target == null)
        {
            IsAttacking = false;
            StartCoroutine(seaching());
            return;
        }
        m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
        var look = Quaternion.LookRotation(pos - m_transform.position);
        m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, look, AngleSpeed * Time.deltaTime);
        if (Vector3.SqrMagnitude(pos - m_transform.position) < reverseDistance * reverseDistance)
            Reverce();

    }

    public override bool Attack()
    {
        IsAttacking = true;
        StartCoroutine(Fight());
        pos = target.position;
        attack = true;
        speed = StartSpeed;
        return true;
    }


    private void  Reverce()
    {
        if (attack)
        {
            Vector2 direct = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            float x = direct.x * distanceFromTarget + m_transform.localPosition.x;
            float y = direct.y * distanceFromTarget + m_transform.localPosition.y;

            pos = new Vector3(x, y, m_transform.localPosition.z);
            attack = false;
        }
        else
        {
            pos = target.position;
            StartCoroutine(Fight());
            attack = true;
        }
    }


    private IEnumerator Fight()
    {
       
        
        var wait = new WaitForSeconds(0.3f);
        foreach (var r in myRockets)
        {
            yield return wait;
            r.SetTarget(target, power);
           
            if (target == null)
            {
                StartCoroutine(seaching());
                yield break;
            }
                
        }
        yield break;
  
    }

    
}
