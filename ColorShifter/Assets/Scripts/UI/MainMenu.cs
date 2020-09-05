﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject gameUI;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameUI.SetActive(true);
    }
}