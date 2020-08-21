using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigStapleRemoverTargetingRangeController : MonoBehaviour
{
    BigStapleRemoverController parentController;
    NavMeshAgent parentNavMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        parentController = GetComponentInParent<BigStapleRemoverController>();
        parentNavMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerEnemyInteractionController>().isDisguised)
            {
                parentController.aiState = BigStapleRemoverController.StapleRemoverAIState.Attacking;
                parentNavMeshAgent.SetDestination(other.gameObject.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Disengage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerEnemyInteractionController>().isDisguised)
            {
                parentController.aiState = BigStapleRemoverController.StapleRemoverAIState.Attacking;
                parentNavMeshAgent.SetDestination(other.gameObject.transform.position);
            }
            else
            {
                Disengage();
            }
        }
    }

    void Disengage()
    {
        parentController.aiState = BigStapleRemoverController.StapleRemoverAIState.Patrol;
        float minDistance = 9999f;
        int closestPoint = 0;
        for (int i = 0; i < parentController.patrolPoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, parentController.patrolPoints[i]) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, parentController.patrolPoints[i]);
                closestPoint = i;
            }
        }
        parentController.currentPatrolPoint = closestPoint;
    }
}
