using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_nodestroy : MonoBehaviour
{
   	void Awake()
   	{
   		GameObject[] obs = GameObject.FindGameObjectsWithTag("AudioListen");
   		if(obs.Length>1)
   		{
   			Destroy(this.gameObject);
   		}
   		DontDestroyOnLoad(this.gameObject);
    }
}
