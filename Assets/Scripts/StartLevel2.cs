﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStart()
    {
    	SceneManager.LoadScene("Level2");
    }
}
