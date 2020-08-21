using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject[] buttonPlatforms;

    public AudioClip spawnSound;
    public AudioSource sound;
    int numPressed;
    // Start is called before the first frame update
    void Start()
    {
        numPressed = 0;
    }

    void OnEnable() {
        PlatformButton.OnPressed += Increment;
    }

    void OnDisable() {
        PlatformButton.OnPressed -= Increment;
    }

    void Increment() {
        numPressed += 1;
        if (numPressed == buttonPlatforms.Length) {
            // Spawn the platform
            sound.PlayOneShot(spawnSound);
            Instantiate(Prefab, transform.position, transform.rotation);
        }
    }


}
