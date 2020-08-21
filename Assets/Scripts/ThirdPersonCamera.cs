using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // reference to player
    public Transform target;
    // Distance between player and camera
    public Vector3 offset;
    // Start is called before the first frame update
    public float rotateSpeed;

    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horiz = Input.GetAxis("Mouse X") * rotateSpeed;
        float vert = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.Rotate(0, horiz, 0);
        target.Rotate(-vert, horiz, 0);

        Quaternion rotation = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, 0);
        
        transform.position = target.position - (rotation * offset);
        transform.LookAt(target);
    }
}
