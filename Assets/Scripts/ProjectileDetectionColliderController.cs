using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetectionColliderController : MonoBehaviour
{

    private ProjectileAgentController pac;
    // Start is called before the first frame update
    void Start()
    {
        pac = GetComponentInParent<ProjectileAgentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerEnemyInteractionController>().isDisguised)
            {
                pac.aiState = ProjectileAgentController.ProjectileAgentState.Aiming;
                pac.SetTarget(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pac.aiState = ProjectileAgentController.ProjectileAgentState.Idle;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerEnemyInteractionController>().isDisguised)
            {
                pac.aiState = ProjectileAgentController.ProjectileAgentState.Aiming;
                pac.SetTarget(other.transform);
            }
            else
            {
                pac.aiState = ProjectileAgentController.ProjectileAgentState.Idle;
            }
        }
    }
}
