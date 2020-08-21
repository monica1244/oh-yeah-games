using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int health;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.setMaxValue(maxHealth);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Stapler")) {
            sound.Play();
            health -= 20;
            healthBar.setValue(health);
        }
    }
}
