using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horiz, 0, vert) * speed * Time.deltaTime;
        
        rb.MovePosition(rb.position + rb.transform.TransformDirection(movement));
    }
}
