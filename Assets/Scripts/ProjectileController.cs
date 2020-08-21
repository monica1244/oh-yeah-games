using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float lifespan;

    private float lifetime = 0f;

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add damage to player before destruction
            SpawnManager spawnManager = other.gameObject.GetComponent<SpawnManager>();
            spawnManager.Damage(10);
            
            Destroy(gameObject);
        }
    }
}
