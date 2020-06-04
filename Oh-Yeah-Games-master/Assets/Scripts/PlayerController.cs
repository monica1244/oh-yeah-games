using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject parent;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 movement;
    private bool GCActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInParent<Animator>();
        GCActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (anim.GetBool("isGrounded"))
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement = movement * 0.2f;
        }
        //anim.SetBool("isGrounded", transform.position.y < 2f);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Short Jump"))
        {
            SetLocalFall();
            rb.AddRelativeForce(movement * 3f);
        }        
    }

    void OnTriggerEnter (Collider other) 
    {
        if (other.CompareTag("GravityCube") && GCActive) 
        {
            Physics.gravity = new Vector3(-1.0f, 0.0f, 0.0f);
            parent.transform.Rotate(0.0f,0.0f,-90.0f);
            SetLocalFall();
            
            GCActive = false;
        }

        if (other.CompareTag("Floor"))
        {
            anim.SetBool("isGrounded", true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            anim.SetBool("isGrounded", false);
        }
    }


    /* Set the player to fall relative to its local y-axis
    Description: Convert intended velocity to local transform orientation, set the new velocity, and then translate back to world orientation
    */
    void SetLocalFall()
    {
        var locVel = transform.InverseTransformDirection(rb.velocity);
        locVel = new Vector3(rb.velocity.x, 10.0f, rb.velocity.z);
        rb.velocity = transform.TransformDirection(locVel);
    }
}
