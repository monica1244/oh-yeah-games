using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewGameControls : MonoBehaviour
{
    // Start is called before the first frame update
    public void ViewControls()
    {
    	SceneManager.LoadScene("KeyboardControls");
    }
}
