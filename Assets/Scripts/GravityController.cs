using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adds custom gravity to a Rigidbody. Make sure to disable gravity on rigidbodies using this.
public class GravityController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private Vector3 target; // Gravity Cube transform buffer
    private Vector3 fwd;
    private bool inOrbit = false;
    
    public float gcSpeed = 1; // Speed of movement toward Gravity Cube 
    public Vector3 gravityDirection;

    void Start()
    {
        gravityDirection = new Vector3(0f, -1f, 0f);
        fwd = transform.forward;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        target = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (inOrbit) // If in contact with a GravityCube
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, target, gcSpeed*Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                inOrbit = false;
                AlignToGravity();
                anim.SetTrigger("grow");
            }
        } else 
        {
            rb.AddForce(9.8f * gravityDirection);
        }
    }

    // Changes gravity by cube
    public void ChangeGravity(Vector3 fwdDirection, Vector3 gravDirection, Vector3 center)
    {
        target = center; // Update buffer
        inOrbit = true;
        gravityDirection = gravDirection;
        fwd = fwdDirection;
        rb.velocity = Vector3.zero;
    }

    // Aligns tranform to new gravity direction
    public void AlignToGravity()
    {
        transform.rotation = Quaternion.LookRotation(fwd, -gravityDirection);
    }

    // Explicitly sets and aligns transform without change process
    public void SetGravity(Vector3 direction)
    {
        gravityDirection = direction;
        AlignToGravity();
    }

}
