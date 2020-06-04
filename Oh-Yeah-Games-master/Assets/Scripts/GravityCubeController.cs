using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCubeController : MonoBehaviour
{
    private Vector3 referenceScale;
    private Vector3 scaleChange;
    private Vector3 gravityDirection;

    public float scaleSpeed;
    public float rotationSpeed;
    public float maxScaleChange;
    public float minScaleChange;

    void Start() 
    {
        referenceScale = transform.localScale;
        scaleChange = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) / 100.0f; // Convert to micro increments from integers
        gravityDirection = new Vector3(0.0f, 0.0f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x - referenceScale.x > maxScaleChange || transform.localScale.x - referenceScale.x < minScaleChange) 
            { 
                scaleChange = scaleChange*(-1.0f);
            }

        transform.Rotate(0.0f, rotationSpeed, 0.0f);
        transform.localScale += scaleChange;
    }

    public Vector3 getDirection()
    {
        return gravityDirection;
    }

}
