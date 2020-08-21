using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    
    public bool onGround;
    public bool jumpClicked;
    private bool justReachedSS;
    private bool justReachedBS;
    private bool justReachedSB;
    private bool justReachedBB;
    public float speed;
    public int score;
    public float bigJumpVelocity = 14f;
    public float smallJumpVelocity = 8f;
    public float RotateSpeed = 30f;
    public AudioClip smallJumpSound;
    public AudioClip coinSound;
    public AudioSource sound;
    public Text Score;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        onGround = true;
        score = 0;
        justReachedSS = true;
        justReachedBS = true;
        justReachedBS = true;
        justReachedBB = true;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.ResetTrigger("OnGround");
        anim.ResetTrigger("MedJump");
        anim.ResetTrigger("HighJump");
        jumpClicked = false;
        anim.SetTrigger("Ss");
        Score.text = "Score:  "+ score.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetTrigger("Ss");

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.AddForce(transform.forward * vertical * speed * Time.deltaTime);

        // Uncomment below for no rotation
        // rb.AddForce(transform.right * horizontal * speed * Time.deltaTime);

        // Comment out below for no rotation
        transform.Rotate(Vector3.up*horizontal*RotateSpeed*Time.deltaTime);

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash")&&justReachedSS)
        {
            justReachedSS = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash"))
        {
            justReachedSS = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash")&&justReachedBS)
        {
            justReachedBS = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash"))
        {
            justReachedBS = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("SmallToBigSquash")&&justReachedSB)
        {
            justReachedSB = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SmallToBigSquash"))
        {
            justReachedSB = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Big Squash")&&justReachedBB)
        {
            justReachedBB = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Big Squash"))
        {
            justReachedBB = true;
        }
        if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch"))
        {
            rb.velocity = smallJumpVelocity*transform.up;
            onGround = false;
        }
        else if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch"))
        {
            rb.velocity = bigJumpVelocity*transform.up;
            jumpClicked = false;
            onGround = false;
            anim.ResetTrigger("SqtSq");
            anim.ResetTrigger("JumpAgain");
        }
        if(Input.GetButton("Jump"))
        {
            RaycastHit hit = new RaycastHit();
            // Raycasts  based on the local "down" position
            if (Physics.Raycast (transform.position, -transform.up, out hit)) 
            {
                var distanceToGround = hit.distance;
                if(distanceToGround<2f)
                {
                    jumpClicked = true;
                    
                }
            }
        }
        if(jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash"))
        {
            anim.SetTrigger("SqtSq");
            anim.ResetTrigger("Ss");
            jumpClicked = false;
        }
        else if(jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash"))
        {
            anim.SetTrigger("JumpAgain");
            anim.ResetTrigger("Ss");
            jumpClicked = false;
        }
    }

    void OnCollisionEnter(Collision any)
    {
        if(any.gameObject.CompareTag("Ground")&&jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch"))
        {
            onGround = true;
            anim.SetTrigger("MedJump");
        }
        else if(any.gameObject.CompareTag("Ground")&&jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch"))
        {
            onGround = true;
            anim.SetTrigger("HighJump");
        }
        else if(any.gameObject.CompareTag("Ground")&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch"))
        {
            onGround = true;
            anim.SetTrigger("BtS");
        }
        else if(any.gameObject.CompareTag("Ground")&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch"))
        {
            onGround = true;
            anim.SetTrigger("OnGround");
        }
        if(any.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            if(Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.JoystickButton1))
            {
                jumpClicked = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            sound.PlayOneShot(coinSound);
            Destroy(other.gameObject);
            score = score + 1;
            Score.text = "Score:  "+(score*10).ToString();
        }
    }

}