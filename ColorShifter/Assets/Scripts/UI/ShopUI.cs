using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    // Variables
    public GameObject mainMenuUI;
    public GameObject shopUI;

    public void ShopUp()
    {
        mainMenuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void MainMenuUp()
    {
        shopUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
