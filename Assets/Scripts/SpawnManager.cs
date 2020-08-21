using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 spawnpoint;
    private Vector3 up;
    private Animator anim;
    public GameObject[] lamps;
    public AudioSource sound;
    public AudioSource healthSound;
    public AudioSource damageSound;
    public AudioSource dyingSound;
    Rigidbody rb;
    bool respawning = false;

    public HealthBar healthBar;
    public int maxHealth = 100;
    public int health;

    void Start()
    {
        spawnpoint = this.transform.position;
        up = player.transform.up;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        health = maxHealth;
        healthBar.setMaxValue(maxHealth);
    }

    void setSpawnPoint(Vector3 position) 
    {
        spawnpoint = position;
        up = player.transform.up;
    }

    public void Respawn() 
    {

        // Reset gravity cubes on respawn
        GravityCubeController[] gravityCubes = FindObjectsOfType(typeof(GravityCubeController)) as GravityCubeController[];
        foreach (GravityCubeController gc in gravityCubes)
        {
            gc.ResetTrigger();
        }

        // Respawn with max health???
        health = maxHealth;
        healthBar.setValue(health);
        respawning = true;
        player.SetActive(false);
        //yield return new WaitForSeconds(1);
        player.transform.position = spawnpoint;
        player.transform.up = up;
        GravityController playerGravityController = this.GetComponent<GravityController>();
        playerGravityController.SetGravity(-up);
        
        // lose coins
        TestMovement tm = player.GetComponent<TestMovement>();
        tm.decreaseScore();
        
        player.SetActive(true);
        anim.SetBool("Ss", true);
        respawning = false;

    }

    void increaseHealth() {
        health = Math.Min(maxHealth, health + 20);
        healthBar.setValue(health);
    }

    public void Damage(int val = 15) 
    {
        PlayerEnemyInteractionController pc = player.GetComponent<PlayerEnemyInteractionController>();
        if (!pc.isDisguised) {
            health -= val;
            damageSound.Play();
            healthBar.setValue(health);
            if (health <= 0){ 
                dyingSound.Play();
                Respawn();
            }
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("KillPlane")) 
        {
            if (!respawning)
                //StartCoroutine("Respawn");
                Respawn();
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lamp")) 
        {
            Light li = other.gameObject.GetComponent<Light>();
            if(!li.enabled)
            {
                sound.Play();
                foreach(GameObject l in lamps)
                {
                    Light light = l.GetComponent<Light>();
                    light.enabled = false;
                }
                li.enabled = true;
                setSpawnPoint(other.gameObject.transform.GetChild(0).gameObject.transform.position);
            }    
        }
        else if (other.gameObject.CompareTag("Health")) 
        {
            if (health < maxHealth) 
            {
                healthSound.Play();
                increaseHealth();
                other.gameObject.transform.parent.gameObject.SetActive(false);
            } 
        }
    }


}