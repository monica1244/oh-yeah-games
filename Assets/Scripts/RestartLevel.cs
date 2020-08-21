using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
 
public class RestartLevel : MonoBehaviour{
    public void GameStart(){
    		Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
    }
}
