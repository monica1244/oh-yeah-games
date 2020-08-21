using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    BigStapleRemoverController parentController;
    public AudioSource sound;
    void Start()
    {
        parentController = transform.root.gameObject.GetComponent<BigStapleRemoverController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sound.Play();
            parentController.Damage();
        }
    }
}
