using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{

	public int score;
	private Text scoreText;

	private int lv1Score;
	private int lv2Score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        GameObject[] obs = GameObject.FindGameObjectsWithTag("ScoreStorage");
        lv1Score = obs[0].GetComponent<DoNotDestroyScore>().GetLevel1Score();
        lv2Score = obs[0].GetComponent<DoNotDestroyScore>().GetLevel2Score();
    }

    // Update is called once per frame
    void Update()
    {
    	int totalScore = lv1Score + lv2Score;
        scoreText.text = "Congrats!\n You scored : " + totalScore;
    }


}
