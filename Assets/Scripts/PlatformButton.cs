using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public delegate void PressAction();
    public static event PressAction OnPressed;
    Renderer r;
    public AudioClip pressSound;
    public AudioSource sound;
    bool pressed = false;

    void Start() {
        r = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!pressed && OnPressed != null) {
                sound.PlayOneShot(pressSound);
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                r.material.SetColor("_Color", Color.yellow);
                pressed = true;

                OnPressed();
            }
        }
    }
}
