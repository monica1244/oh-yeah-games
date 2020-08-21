using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StapleRemoverController : MonoBehaviour
{

    public enum StapleRemoverAIState
    {
        Patrol,
        Attacking
    }

    public StapleRemoverAIState aiState;
    public Vector3[] patrolPoints;
    public int currentPatrolPoint = 0;
    
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        aiState = StapleRemoverAIState.Patrol;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint]);
    }

    // Update is called once per frame
    void Update()
    {
        switch(aiState)
        {
            case StapleRemoverAIState.Patrol:
                if (navMeshAgent.remainingDistance < 1 && !navMeshAgent.pathPending)
                {
                    currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint]);
                }
                break;

            case StapleRemoverAIState.Attacking:
                break;
        }
    }

    public void Stomp()
    {
        navMeshAgent.isStopped = true;
        Destroy(gameObject);
    }    
}
