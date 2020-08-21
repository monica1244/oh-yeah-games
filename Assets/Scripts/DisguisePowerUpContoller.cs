using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguisePowerUpContoller : MonoBehaviour
{
    public float disguiseTime = 5f;
    public float scaleSpeed = 0.1f;
    public float rotationSpeed = 0.5f;
    public float maxScaleChange = 0.1f;
    public float minScaleChange = -0.1f;
    public AudioSource ss;
    private Vector3 scaleChange;
    private Vector3 referenceScale;

    void Update()
    {
        if (transform.localScale.x - referenceScale.x > maxScaleChange || transform.localScale.x - referenceScale.x < minScaleChange) 
            { 
                scaleChange = scaleChange*(-1.0f);
            }

        transform.Rotate(0.0f, rotationSpeed, 0.0f);
        transform.localScale += scaleChange;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ss.Play();
            var EnemyInterCont = other.GetComponent<PlayerEnemyInteractionController>();
            EnemyInterCont.ActivateDisguise(disguiseTime);
        }

        this.gameObject.SetActive(false);
    }
}
