using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewRules : MonoBehaviour
{
    // Start is called before the first frame update
    public void SeeRules()
    {
    	SceneManager.LoadScene("Rules Scene");
    } 
}
