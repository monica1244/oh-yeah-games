using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanObstacle : MonoBehaviour
{
    public float speed = 60;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate((speed * Time.deltaTime), 0, 0);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnManager spawnManager = other.gameObject.GetComponent<SpawnManager>();
            spawnManager.Damage(10);
        }
    }
}
