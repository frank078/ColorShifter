using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameUI;

    private void Start()
    {
        // Reminder: The logic is reversed in order to achieve the retart
        if (GameManager.Instance.isRestart == true)
        {
            Time.timeScale = 0;           
        }
        else
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameUI.SetActive(true);
        GameManager.Instance.isRestart = false;
    }
}
