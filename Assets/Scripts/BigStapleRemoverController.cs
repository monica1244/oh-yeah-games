using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BigStapleRemoverController : MonoBehaviour
{

    public enum StapleRemoverAIState
    {
        Patrol,
        Attacking
    }

    public StapleRemoverAIState aiState;
    public Vector3[] patrolPoints;
    public int currentPatrolPoint = 0;

    
    public HealthBar healthBar;
    public int maxHealth = 100;

    public int health;
    
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        aiState = StapleRemoverAIState.Patrol;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint]);
        health = 100;
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
        SceneManager.LoadScene("LevelTransition");
    }

    public void Damage() {
        health = health - 10;
        healthBar.setValue(health);
        if (health <= 0) {
            Stomp();
        }

    }    
}
