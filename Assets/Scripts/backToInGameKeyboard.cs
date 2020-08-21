using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backToInGameKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;
    private CanvasGroup myGroup;
    // Start is called before the first frame update
	void Awake()
	{
		myGroup = this.transform.parent.GetComponent<CanvasGroup>();
		canvasGroup = this.transform.parent.transform.parent.GetChild(7).GetComponent<CanvasGroup>();
		if(canvasGroup==null)
		{
			Debug.LogError("No Canvas Group component");
		}
	}
    // Update is called once per frame
    public void view()
    {
    	myGroup.interactable = false;
    	myGroup.blocksRaycasts = false;
    	myGroup.alpha = 0f;
		canvasGroup.interactable = true; 
		canvasGroup.blocksRaycasts = true; 
		canvasGroup.alpha = 1f;
    }
}
