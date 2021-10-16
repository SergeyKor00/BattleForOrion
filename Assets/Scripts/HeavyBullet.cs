using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : MonoBehaviour
{
    private Transform target, myParent, m_transform;

    private bool IsMoving;

    public float speed;

    private int power;

    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        
        myParent = m_transform.parent;
        child = m_transform.GetChild(0).gameObject;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


   
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform == target)
        {
            target.GetComponent<SceneObject>().Health -= power;

            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform targ, float startSpeed, int power = 0)
    {
        target = targ;
        m_transform = transform;
        m_transform.LookAt(target);
        speed = startSpeed;
        this.power = power;
        //speed = startSpeed;
        
    }
}
