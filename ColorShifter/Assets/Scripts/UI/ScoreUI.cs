using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // call textmeshpro

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int currentScore;

    void Start()
    {
        GameManager.Instance.OnWallsCollided += ModifyScore;

        scoreText = gameObject.GetComponent<TextMeshProUGUI>();

        scoreText.text = "0"; //TODO: SAVE SYSTEM LOAD SCORE
    }

    void ModifyScore(int playerScore)
    {
        currentScore = playerScore;
        scoreText.text = currentScore.ToString();
    }
}
