using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteHitboxController : MonoBehaviour
{
    StapleRemoverController parentController;
    // Start is called before the first frame update
    void Start()
    {
        parentController = GetComponentInParent<StapleRemoverController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaycastHit hit = new RaycastHit();
            int NPCMask = (1 << 10); 
            if (Physics.Raycast ((this.transform.parent.transform.position- this.transform.parent.transform.forward), this.transform.parent.transform.forward*5,  out hit, Mathf.Infinity, NPCMask)) 
            {
                SpawnManager spawnManager = other.GetComponent<SpawnManager>();
                spawnManager.Damage(30);
                parentController.aiState = StapleRemoverController.StapleRemoverAIState.Patrol;
            }
        }
    }
}
