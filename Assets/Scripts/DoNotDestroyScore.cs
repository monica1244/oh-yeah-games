using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyScore : MonoBehaviour
{
    private int level1Score = 0;
    private int level2Score = 0;

    void Awake()
    {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("ScoreStorage");
        if (obs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetLevelScore(string sceneName, int score)
    {
        if (sceneName.Equals("Level1"))
        {
            level1Score = score;
        } else if (sceneName.Equals("Level2"))
        {
            level2Score = score;
        }
    }

    public int GetLevel1Score()
    {
        return level1Score;
    }

    public int GetLevel2Score()
    {
        return level2Score;
    }
}
