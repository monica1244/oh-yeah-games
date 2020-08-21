using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backToInGameMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
	void Awake()
	{
		canvasGroup = this.transform.parent.GetComponent<CanvasGroup>();
		if(canvasGroup==null)
		{
			Debug.LogError("No Canvas Group component");
		}
	}
    public void back()
    {
    	if (canvasGroup.interactable) 
			{
				canvasGroup.interactable = false; 
				canvasGroup.blocksRaycasts = false; 
				canvasGroup.alpha = 0f;
			}
    }
}
