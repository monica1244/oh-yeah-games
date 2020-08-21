using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseButton : MonoBehaviour
{
	private CanvasGroup canvasGroup;
    // Start is called before the first frame update
	void Awake()
	{
		canvasGroup = this.transform.parent.GetChild(1).GetComponent<CanvasGroup>();
		if(canvasGroup==null)
		{
			Debug.LogError("No Canvas Group component");
		}
	}
    // Start is called before the first frame update
    public void pauseGame()
    {
    	canvasGroup.interactable = true; 
		canvasGroup.blocksRaycasts = true; 
		canvasGroup.alpha = 1f;
		Time.timeScale = 0f;
    }
}
