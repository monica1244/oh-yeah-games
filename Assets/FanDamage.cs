using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnManager spawnManager = other.gameObject.GetComponent<SpawnManager>();
            spawnManager.Damage(10);
        }
    }
}
