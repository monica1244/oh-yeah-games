using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFallingSound : MonoBehaviour
{
    
    public AudioSource sound;
    void OnTriggerEnter(Collider any) {
        if (any.CompareTag("Player")) {
            sound.Play();
        }
    }
}
