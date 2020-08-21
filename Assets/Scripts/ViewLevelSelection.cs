using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewLevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public void view()
    {
    	SceneManager.LoadScene("Level Select");
    }
}
