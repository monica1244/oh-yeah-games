using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompHitboxController : MonoBehaviour
{
    public bool goingUp;
    StapleRemoverController parentController;
    public GameObject player;
    public AudioSource sound;
    float lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        goingUp = false;
        parentController = GetComponentInParent<StapleRemoverController>();
    }

    void FixedUpdate()
    {
        var velocity = Vector3.Dot(player.transform.position,player.transform.up) - lastPosition;
        lastPosition = Vector3.Dot(player.transform.position,player.transform.up);
        if (velocity > 0.01)
        {
            goingUp = true;
        }
        else if (velocity < -0.01)
        {
            goingUp = false;
        }
        // Debug.DrawRay((this.transform.parent.transform.position- this.transform.parent.transform.forward), this.transform.parent.transform.forward*5, Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player")&&!goingUp)
        {
            RaycastHit hit = new RaycastHit();
            int NPCMask = (1 << 10); 
            if (!Physics.Raycast ((this.transform.parent.transform.position- this.transform.parent.transform.forward), this.transform.parent.transform.forward*5,  out hit, Mathf.Infinity, NPCMask)) 
            {
                sound.Play();
                parentController.Stomp();
            }
        }
    }
}
