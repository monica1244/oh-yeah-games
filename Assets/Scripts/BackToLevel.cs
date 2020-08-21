using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToLevel : MonoBehaviour
{

	public string levelScene;
	public void travelToLevel() {
		SceneManager.LoadScene(levelScene);
	}
}
