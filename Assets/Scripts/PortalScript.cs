using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool levelComplete = false;
    public string nextScene;
    public Text Score;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            //levelComplete = true;
            SceneManager.LoadScene(nextScene);
        }
    }

    // private void OnGUI() {
    //     if (levelComplete) {
    //         //GUI.color = Color.green;
    //         GUI.Window(078, new Rect(Screen.width/2 - 300, Screen.width/2 - 450, 200, 100), windowFunc, "Congrats!");
    //     }
    // }

    // void windowFunc(int windowID) {
    //     GUI.backgroundColor = Color.gray;
    //     GUI.Label(new Rect(Screen.width/2 - 200, Screen.width/2 - 400, 100, 50), "Level Complete!");
    //     GUI.Label(new Rect(Screen.width/2 - 200, Screen.width/2 - 300, 100, 50), Score.text);
    //     if (GUI.Button(new Rect(Screen.width/2 - 200, Screen.width/2 - 50, 100, 50), "Quit")) {
    //         Application.Quit();
    //     }
    //     //SceneManager.LoadScene(scenename);
    // }
}
