using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class EscapeButton : MonoBehaviour
{
	private CanvasGroup canvasGroup;
    // Start is called before the first frame update
	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		if(canvasGroup==null)
		{
			Debug.LogError("No Canvas Group component");
		}
	}
    // Update is called once per frame
    void Update()
    {
   		if (Input.GetButtonUp("Pause")) 
       	{
			if (canvasGroup.interactable) 
			{
				canvasGroup.interactable = false; 
				canvasGroup.blocksRaycasts = false; 
				canvasGroup.alpha = 0f;
				Time.timeScale = 1f;
			} else {
				canvasGroup.interactable = true; 
				canvasGroup.blocksRaycasts = true; 
				canvasGroup.alpha = 1f;
				Time.timeScale = 0f;
			} 
		} 
    }
}
