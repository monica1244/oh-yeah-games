using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    public float waitTime;
    public float vanishTime;

    private Vector3 scale;

    private void Start() {
        scale = transform.localScale;
        StartCoroutine(vanish());
    }

    IEnumerator vanish() {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(vanishTime);
            transform.localScale = scale;
        }
        
    }

}
