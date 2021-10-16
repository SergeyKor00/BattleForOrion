using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private Vector3  target;

    private Transform m_base, m_transform, asteroid;

    private bool toBase;

    public float speed;

    public GameObject lazer;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(Vector3.SqrMagnitude(target - m_transform.position) < 1.0f)
        {
            StartCoroutine(waiting());
        }
    }
    
   
    private IEnumerator waiting()
    {
        if (m_base == null)
            Destroy(gameObject);
        float defSpeed = speed;
        speed = 0.0f;
        if(toBase)
        {
            toBase = false;
            target = asteroid.position;
            yield return new WaitForSeconds(1.0f);
            transform.rotation = Quaternion.LookRotation(target - m_transform.position);
            speed = defSpeed;
        }
        else
        {
            toBase = true;
            lazer.SetActive(true);
            target = m_base.position;
            yield return new WaitForSeconds(2.5f);
            transform.rotation = Quaternion.LookRotation(target - m_transform.position);
            lazer.SetActive(false);
            speed = defSpeed;
        }
        
    }
    public void SetPoint(Transform a, Transform b)
    {
        asteroid = a;
        m_base = b;
        toBase = false;
        target = asteroid.position;
        transform.rotation = Quaternion.LookRotation(target - transform.position);
    }
}
