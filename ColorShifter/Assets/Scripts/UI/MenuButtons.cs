using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject pauseMenu;

    public void pauseButton()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
