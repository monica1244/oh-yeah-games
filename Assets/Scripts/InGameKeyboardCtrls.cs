using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameKeyboardCtrls : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
	void Awake()
	{
		canvasGroup = this.transform.parent.transform.parent.GetChild(7).GetComponent<CanvasGroup>();
		if(canvasGroup==null)
		{
			Debug.LogError("No Canvas Group component");
		}
	}
    // Update is called once per frame
    public void view()
    {
		canvasGroup.interactable = true; 
		canvasGroup.blocksRaycasts = true; 
		canvasGroup.alpha = 1f;
    }
}