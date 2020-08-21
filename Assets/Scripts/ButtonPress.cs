using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource sound;
    void OnCollisionEnter(Collision any) {
        if (any.gameObject.CompareTag("Player")) {
            sound.Play();
        }
    }
}
