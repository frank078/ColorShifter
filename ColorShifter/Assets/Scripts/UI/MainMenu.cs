using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameUI;
    public TextMeshProUGUI highScore;

    private void Start()
    {
        if (GameManager.Instance.isRestart == true)
        {
            StartGame();                     
        }
        else
        {
            Time.timeScale = 0;
        }

        highScore.text = "HighScore\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameUI.SetActive(true);
    }
}
