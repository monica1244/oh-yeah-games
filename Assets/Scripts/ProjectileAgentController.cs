using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAgentController : MonoBehaviour
{

    public Rigidbody projectile;
    public enum ProjectileAgentState
    {
        Idle,
        Aiming
    }
    public ProjectileAgentState aiState;
    public float fireCooldown;
    public float projectileSpeed;
    public AudioSource sound;
    private float timeSinceLastFire;
    private Transform target;


    private void Start()
    {
        aiState = ProjectileAgentState.Idle;
        timeSinceLastFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case ProjectileAgentState.Idle:
                break;

            case ProjectileAgentState.Aiming:
                Vector3 sameHeightTarget = target.position;
                if (transform.up.x != 0)
                {
                    sameHeightTarget.x = transform.position.x;
                } else if (transform.up.y != 0)
                {
                    sameHeightTarget.y = transform.position.y;
                } else
                {
                    sameHeightTarget.z = transform.position.z;
                }
                transform.LookAt(sameHeightTarget, transform.up);
                timeSinceLastFire += Time.deltaTime;
                if (timeSinceLastFire > fireCooldown)
                {
                    sound.Play();
                    FireProjectile();
                    timeSinceLastFire = 0f;
                }
                break;
        }
    }

    void FireProjectile()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.gameObject.SetActive(true);
        clone.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
