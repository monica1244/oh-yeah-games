using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestMovement : MonoBehaviour
{
    
    public bool onGround;
    public bool jumpClicked;
    private bool justReachedSS;
    private bool justReachedBS;
    private bool justReachedSB;
    private bool justReachedBB;
    public bool goingUp;
    public float lastPosition;
    public float speed;
    public int score;
    public float bigJumpVelocity = 14f;
    public float smallJumpVelocity = 8f;
    private float RotateSpeed = 40f;
    private bool squashedLast;
    public AudioClip smallJumpSound;
    public AudioClip coinSound;
    public AudioSource sound;
    public AudioSource bigBounce;
    public Text Score;
    int NPCMask = ~(1 << 10); 

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        onGround = true;
        score = 0;
        goingUp = false;
        justReachedSS = true;
        justReachedBS = true;
        justReachedBS = true;
        justReachedBB = true;
        squashedLast = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.ResetTrigger("OnGround");
        anim.ResetTrigger("MedJump");
        anim.ResetTrigger("HighJump");
        jumpClicked = false;
        anim.SetBool("Ss", true);
        Score.text = "Score:  "+ score.ToString();
        lastPosition = Vector3.Dot(transform.position,transform.up);
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton1)||Input.GetMouseButtonDown(0))&&!goingUp)
        {
            RaycastHit hit = new RaycastHit();
            // Raycasts  based on the local "down" position
            if (Physics.Raycast (this.transform.position, -this.transform.up,  out hit, Mathf.Infinity, NPCMask)) 
            {
                var distanceToGround = hit.distance;
                if(distanceToGround<2.8f)
                {
                    jumpClicked = true;
                }
            }
        }
        if((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton1)||Input.GetMouseButtonDown(0))&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash"))
        {
            anim.SetBool("Ss", false);
            anim.SetTrigger("SqtSq");
            jumpClicked = false;
        }
        else if((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton1)||Input.GetMouseButtonDown(0))&&anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash"))
        {
            anim.SetBool("Ss", false);
            anim.SetTrigger("JumpAgain");
            jumpClicked = false;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash")||anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash")||anim.GetCurrentAnimatorStateInfo(0).IsName("SmallToBigSquash")||anim.GetCurrentAnimatorStateInfo(0).IsName("Big Squash"))
        {
            squashedLast = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash")&&justReachedSS&&!this.GetComponent<PlayerEnemyInteractionController>().isDisguised)
        {
            justReachedSS = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Small Squash"))
        {
            justReachedSS = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash")&&justReachedBS&&!this.GetComponent<PlayerEnemyInteractionController>().isDisguised)
        {
            justReachedBS = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("BigToSmallSquash"))
        {
            justReachedBS = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("SmallToBigSquash")&&justReachedSB&&!this.GetComponent<PlayerEnemyInteractionController>().isDisguised)
        {
            justReachedSB = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SmallToBigSquash"))
        {
            justReachedSB = true;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Big Squash")&&justReachedBB&&!this.GetComponent<PlayerEnemyInteractionController>().isDisguised)
        {
            justReachedBB = false;
            sound.PlayOneShot(smallJumpSound);
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Big Squash"))
        {
            justReachedBB = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var velocity = Vector3.Dot(transform.position,transform.up) - lastPosition;
        lastPosition = Vector3.Dot(transform.position,transform.up);
        if (velocity > 0.01)
        {
            goingUp = true;
        }
        else if (velocity < -0.01)
        {
            goingUp = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rot = Input.GetAxis("Mouse X");

        rb.AddForce(transform.forward * vertical * speed * Time.deltaTime);
        rb.AddForce(transform.right * horizontal * speed * Time.deltaTime);

        transform.Rotate(Vector3.up, rot*RotateSpeed*Time.deltaTime);

        if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch")&&squashedLast)
        {
            rb.velocity = smallJumpVelocity*transform.up;
            onGround = false;
            squashedLast = false;
            jumpClicked = false;
        }
        if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch")&&squashedLast)
        {
            rb.velocity = bigJumpVelocity*transform.up;
            jumpClicked = false;
            onGround = false;
            squashedLast = false;
            anim.SetBool("Ss",true);
            anim.ResetTrigger("SqtSq");
            anim.ResetTrigger("JumpAgain");
            anim.ResetTrigger("MedJump");
        }
    }

    void OnCollisionEnter(Collision any)
    {
        if(any.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            if(onGround&&jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch"))
            {
                anim.SetTrigger("MedJump");
            }
            else if(onGround&&jumpClicked&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch"))
            {
                anim.SetTrigger("HighJump");
            }
            else if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Big Stretch"))
            {
                anim.SetTrigger("BtS");
            }
            else if(onGround&&anim.GetCurrentAnimatorStateInfo(0).IsName("Small Stretch"))
            {
                anim.SetTrigger("OnGround");
            }
        } else if(any.gameObject.CompareTag("JumpBoost")) {
            bigBounce.Play();
            rb.AddForce (Vector3.up * 510f);
        } else if(any.gameObject.CompareTag("StaplerPush")) {
            rb.AddForce (Vector3.up * 510f);
        }
    }

    void OnCollisionStay(Collision any)
    {
        if(any.gameObject.CompareTag("Ground"))
        {
            if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton1)||Input.GetMouseButtonDown(0))
            {
                jumpClicked = true; 
            }
        }
    }

    public void decreaseScore() {
        score = Math.Max(0, score - 2);
        Score.text = "Score:  "+(score*10).ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            sound.PlayOneShot(coinSound);
            Destroy(other.gameObject);
            score = score + 1;
            Score.text = "Score:  "+(score*10).ToString();
            GameObject[] obs = GameObject.FindGameObjectsWithTag("ScoreStorage");
            obs[0].GetComponent<DoNotDestroyScore>().SetLevelScore(SceneManager.GetActiveScene().name, score * 10);
        }
    }

}
