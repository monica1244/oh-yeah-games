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
    private string floorTag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GCActive = true; // Can the cube still change gravity?
        floorTag = "FloorA";
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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Short Jump"))
        {
            rb.AddRelativeForce(movement * 3f);
            float jumpSpeed = 5f;
            if (Input.GetButton("Jump"))
            {
                jumpSpeed = 10f;
            }
            if (GCActive)
            {
                SetLocalJump(jumpSpeed);
            }
            else
            {
                SetLocalJump(jumpSpeed * -1f);
            }
        }
    }

    void OnTriggerEnter (Collider other) 
    {
        if (other.CompareTag("GravityCube") && GCActive) 
        {
            Physics.gravity = new Vector3(-9.8f, 0.0f, 0.0f);
            transform.RotateAround (transform.position, new Vector3(0,0,1), 90.0f);
            SetLocalFall();
            floorTag = "FloorB"; // Change tag to match new orientation
            
            GCActive = false;
        }

        if (other.CompareTag(floorTag))
        {
            anim.SetBool("isGrounded", true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag(floorTag))
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

    void SetLocalJump(float speed)
    {
        var localVel = transform.InverseTransformDirection(rb.velocity);
        localVel.y = speed;
        rb.velocity = transform.TransformDirection(localVel);
    }
}
