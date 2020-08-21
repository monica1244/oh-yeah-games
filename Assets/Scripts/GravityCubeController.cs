using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GravityCubeController : MonoBehaviour
{
    private Vector3 referenceScale;
    private Vector3 scaleChange;
    private Vector3 gravVector;
    private Vector3 fwdVector;
    private Direction currentDirection;
    private bool hasBeenTriggered;

    public enum Direction // To simplify gravity direction choices
    {
        DOWN,
        UP,
        NORTH,
        SOUTH,
        EAST,
        WEST
    };

    public float scaleSpeed = 0.1f;
    public float rotationSpeed = 0.5f;
    public float maxScaleChange = 0.1f;
    public float minScaleChange = -0.1f;
    public AudioSource sound;
    public Direction fwdDirection;
    public ParticleSystem ps;
    public Direction gravDirection = Direction.DOWN;

    private void AssertQuit(bool condition, string message)
    {
        if (!condition)
        {
            Debug.Log(message);
            Debug.Break();
        }
    }

    void Start() 
    {
        gravVector = DirectionToVec3(gravDirection); // Interprets ENUM direction choice
        fwdVector = DirectionToVec3(fwdDirection);

        AssertQuit(Vector3.Dot(gravVector, fwdVector) == 0, "Forward and gravity vectors are not perpendicular: check GravityCubeController"); // Require the vectors to be perp

        referenceScale = transform.localScale;
        scaleChange = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) / 100.0f; // Convert to micro increments from integers
        ps.transform.rotation = Quaternion.LookRotation(fwdVector, gravVector);
        currentDirection = gravDirection; // Save current orientation for reference
        hasBeenTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentDirection != gravDirection) 
        {
            gravVector = DirectionToVec3(gravDirection); // Interprets ENUM direction choice
            fwdVector = DirectionToVec3(fwdDirection);

            AssertQuit(Vector3.Dot(gravVector, fwdVector) == 0, "Forward and gravity vectors are not perpendicular: check GravityCubeController"); // Require the vectors to be perp

            ps.transform.rotation = Quaternion.LookRotation(fwdVector, gravVector);
            currentDirection = gravDirection;
        }

        if (transform.localScale.x - referenceScale.x > maxScaleChange || transform.localScale.x - referenceScale.x < minScaleChange) 
            { 
                scaleChange = scaleChange*(-1.0f);
            }

        transform.Rotate(0.0f, rotationSpeed, 0.0f);
        transform.localScale += scaleChange;
    } 

    private Vector3 DirectionToVec3(Direction dir)
    {
        switch (dir)
        {
            case Direction.UP:
            return new Vector3 (0f, 1f, 0f);
            case Direction.DOWN:
            return new Vector3 (0f, -1f, 0f);
            case Direction.NORTH:
            return new Vector3 (0f, 0f, 1f);
            case Direction.SOUTH:
            return new Vector3 (0f, 0f, -1f);
            case Direction.EAST:
            return new Vector3 (-1f, 0f, 0f);
            case Direction.WEST:
            return new Vector3 (1f, 0f, 0f);
            default: // Should never happen
            Debug.Assert(false, "No direction recognized by DirectionToVec3 method. Check GravityCubeController Script");
            return Vector3.zero; // impossible case -- added to appease the compiler :(
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) { return; } // TODO: find more elegant solution to OnTriggerEnter firing twice
        if (other.CompareTag("Player"))
        {
            sound.Play();
            GravityController playerGravityController = other.gameObject.GetComponent<GravityController>();
            Animator anim = playerGravityController.GetComponent<Animator>();
            anim.SetTrigger("shrink");
            playerGravityController.ChangeGravity(fwdVector, gravVector, transform.position);
            // Debug.Log("Other Collider:" + other.name);
            hasBeenTriggered = true;
        }
    }

    public Direction GetGravityDirection()
    {
        return gravDirection;
    }

    public Vector3 GetGravityVector()
    {
        return gravVector;
    }

    public Direction GetForwardDirection()
    {
        return fwdDirection;
    }

    public Vector3 GetForwardVector()
    {
        return fwdVector;
    }

    public bool ChangeGravity(Direction newGravDirection, Direction newFwdDirection)
    {
        gravDirection = newGravDirection;
        fwdDirection = newFwdDirection;
        return true;
    }

    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }

}
