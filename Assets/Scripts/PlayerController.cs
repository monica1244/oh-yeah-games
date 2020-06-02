using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInParent<Animator>();
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
        anim.SetBool("isGrounded", rb.position.y < 2f);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Short Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5.0f, rb.velocity.z);
            rb.AddForce(movement * 3f);
        }        
    }
}
