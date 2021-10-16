using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target, myParent, m_transform;

    private bool IsMoving;

    public float speed;

    private int power;

    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
        myParent = m_transform.parent;
        child = m_transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            try
            {
                Move();

            }
            catch (System.Exception)
            {
                if (myParent == null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    IsMoving = false;
                    child.gameObject.SetActive(false);
                    m_transform.SetParent(myParent);
                    m_transform.localPosition = Vector3.zero;
                    m_transform.localRotation = Quaternion.identity;
                    m_transform.localScale = Vector3.one;
                }

            }
        }
    }


    private void Move()
    {
        m_transform.LookAt(target);
        m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //speed += 0.1f;
        if (Vector3.SqrMagnitude(target.position - m_transform.position) < 0.25f)
        {
            StartCoroutine(Damage());
        }

    }

    private IEnumerator Damage()
    {

        target.GetComponent<SceneObject>().Health -= power;
        IsMoving = false;
        
        child.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if(myParent == null)
        {
            Destroy(gameObject);
            yield break;
        }
        m_transform.SetParent(myParent);
        m_transform.localPosition = Vector3.zero;
        m_transform.localRotation = Quaternion.identity;
        m_transform.localScale = Vector3.one;
        
    }



    public void SetTarget(Transform targ,  int power = 0)
    {
        target = targ;
        IsMoving = true;
        m_transform.SetParent(null);
        this.power = power;
        //speed = startSpeed;
        child.gameObject.SetActive(true);
    }
}
