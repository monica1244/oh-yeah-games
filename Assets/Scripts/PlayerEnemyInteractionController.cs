using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteractionController : MonoBehaviour
{
    public bool isDisguised;
    private float disguiseTimer = 0f;
    private GameObject hat;

    // Start is called before the first frame update
    void Start()
    {
        isDisguised = false;
        hat = transform.GetChild(0).transform.GetChild(0).gameObject; // hacky but whatever
    }

    void Update()
    {
        if (disguiseTimer > 0)
        {
            disguiseTimer -= Time.deltaTime;
        } else 
        {
            isDisguised = false;
            hat.SetActive(false);
        }
    }

    public void ActivateDisguise(float disguiseTime)
    {
        isDisguised = true;
        disguiseTimer = disguiseTime;
        hat.SetActive(true);
    }
}