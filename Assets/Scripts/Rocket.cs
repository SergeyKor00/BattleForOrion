using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target, myParent, m_transform;

    private bool IsMoving;

    private float speed;

    private int power;

    private ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
        myParent = m_transform.parent;
        fire = GetComponentInChildren<ParticleSystem>();
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
            catch (System.NullReferenceException)
            {
                if (target == null)
                {

                }
                else
                    Destroy(gameObject);

            }
        }
    }
    private void Move()
    {
        m_transform.LookAt(target);
        m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
        speed += 0.1f;
        if(Vector3.SqrMagnitude(target.position - m_transform.position) < 0.25f)
        {
            StartCoroutine(Damage());
        }

    }

    private IEnumerator Damage()
    {

        target.GetComponent<SceneObject>().Health -= power;
        IsMoving = false;
        fire.Stop();
        fire.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        m_transform.SetParent(myParent);
        m_transform.localPosition = Vector3.zero;
        m_transform.localRotation = Quaternion.identity;
        m_transform.localScale = Vector3.one;
        fire.gameObject.SetActive(true);
    }
    public void SetTarget(Transform targ, float startSpeed, int power = 0)
    {
        target = targ;
        IsMoving = true;
        m_transform.SetParent(null);
        this.power = power;
        speed = startSpeed;
        fire.Play();
    }

}
