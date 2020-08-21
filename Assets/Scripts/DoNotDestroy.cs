using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
   	void Awake()
   	{
   		GameObject[] obs = GameObject.FindGameObjectsWithTag("Music");
   		if(obs.Length>1)
   		{
   			Destroy(this.gameObject);
   		}
   		DontDestroyOnLoad(this.gameObject);
    }
}
