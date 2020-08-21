using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBiteHitboxController : MonoBehaviour
{
    BigStapleRemoverController parentController;
    public float fireCooldown = 2f;
    private float timeSinceLastFire;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastFire = 0f;
        parentController = GetComponentInParent<BigStapleRemoverController>();
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&&(timeSinceLastFire > fireCooldown))
        {
            SpawnManager spawnManager = other.GetComponent<SpawnManager>();
            spawnManager.Damage(10);
            timeSinceLastFire = 0f;
            parentController.aiState = BigStapleRemoverController.StapleRemoverAIState.Patrol;
        }
    }
}
