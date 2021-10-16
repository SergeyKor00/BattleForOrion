using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GameUnit : SceneObject
{
    public NavMeshAgent agent;

    protected Transform target;

    public float attackDistance;

    
    protected Transform m_transform;

    private bool IsMoving = false;

    private Transform agentPos;

    public float AngleSpeed;

    public float offset;

    private float height;

    protected float speed;
    protected bool IsAttacking;

    protected float StartSpeed;

    public int power;
    // Start is called before the first frame update
    protected  new void Start()
    {
        m_transform = transform;
        agentPos = agent.transform;
        agentPos.SetParent(null);
        height = m_transform.position.y;

        StartSpeed = agent.speed - 0.5f;
        speed = StartSpeed;
        base.Start();
        StartCoroutine(seaching());
    }

    public void CalculateSpeed()
    {
        StartSpeed = agent.speed - 0.5f;
        speed = StartSpeed;
    }

    protected void Update()
    {
        if (IsMoving)
        {
            Vector3 pos = agentPos.position;

            
            if (agent.pathPending)
                return;

            pos.y = height;
            m_transform.Translate(Vector3.forward * speed * Time.deltaTime);

            Vector3 endPos = agent.pathEndPosition;
            endPos.y = pos.y;
            if(Vector3.SqrMagnitude(endPos - m_transform.position) < Mathf.Pow(agent.stoppingDistance, 2))
            {
                speed -= 0.1f;
                if (speed <= 0)
                {
                    speed = 0.0f;
                    IsMoving = false;
                    Debug.Log("Stopping");
                    agent.ResetPath();
                    StartCoroutine(seaching());
                }
            }
            else
            {
                var look = Quaternion.LookRotation(pos - m_transform.position);
                m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, look, AngleSpeed * Time.deltaTime);
                //m_transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }


        }
    }

    private void StartMoving()
    {
        IsMoving = true;
        IsAttacking = false;
    }

    public override void RightClick(Transform target, bool IsPoint, bool AutoAttack = false)
    {
        StopAllCoroutines();


        if (Input.GetKey(KeyCode.LeftShift) || AutoAttack) {
                StartCoroutine(seaching());
        }
        StartMoving();
        speed = StartSpeed;
        if(IsPoint)
        {
            Vector3 position = target.position;
            if(controller.selectedObj.Count > 1)
                position += new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)) * offset;
            agent.speed = StartSpeed + 0.5f;
            agent.SetDestination(position);
            return;
        }

        if (target.GetComponent<SceneObject>().myType != myType)
        {
            this.target = target;
            agent.SetDestination(target.position);
            StartCoroutine(movingToEnemy());
        }
    }

    private IEnumerator movingToEnemy()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));

        while (true)
        {
            if(target == null)
            {
                agent.ResetPath();
                IsMoving = false;
                yield break;
            }
            
        
            if (Vector3.SqrMagnitude(target.position - m_transform.position) < attackDistance * attackDistance)
            {
                Attack();
                agent.ResetPath();
                IsMoving = false;
                yield break;

            }
            else
            {
                agent.SetDestination(target.position);
                yield return new WaitForSeconds(0.5f);
            }
                

        }

    }


    protected IEnumerator seaching()
    {
        
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        if (agent.hasPath)
        {
            agent.speed = StartSpeed + 0.5f;
            IsMoving = true;
        }
        while (true)
        {
            foreach(var list in UnitsEnum.FirstCreated)
            {
                if(list.Key != myType)
                {
                    foreach(UnitsEnum e in list.Value)
                    {
                        var t = e.transform;
                        if (Vector3.SqrMagnitude(t.position - m_transform.position) < attackDistance * attackDistance)
                        {
                            target = t;
                            Debug.Log("Seacing");
                            if(Attack())
                            {
                                IsMoving = false;
                                agent.speed = 0.0f;
                                yield break;
                            }

                        }
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
            
    }


    public abstract bool Attack();

   
    public void SetSpeed(float value)
    {
        speed = value;

    }

    private void OnDestroy()
    {
        Destroy(agent.gameObject);
    }
}
