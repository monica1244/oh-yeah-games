using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorAnimation : MonoBehaviour
{
    public Animator animationController;

    private void OnCollisionEnter(Collision other) {

    	if(other.gameObject.CompareTag("Player")) {
    		animationController.SetBool("buttonPressed", true);
    	}
    }
}
