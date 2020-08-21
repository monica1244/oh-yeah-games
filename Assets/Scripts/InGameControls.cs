using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class InGameControls : MonoBehaviour
{
	private CanvasGroup canvasGroup;
    // Start is called before the first frame update
	void Awake()
	{
		canvasGroup = this.transform.parent.transform.parent.GetChild(5).GetComponent<CanvasGroup>();
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
