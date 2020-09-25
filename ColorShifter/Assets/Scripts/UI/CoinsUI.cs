using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // call textmeshpro

public class CoinsUI : MonoBehaviour
{
    private TextMeshProUGUI coinsText;
    private int currentCoins;

    void Start()
    {
        GameManager.Instance.OnCoinsCollided += ModifyCoins;

        coinsText = gameObject.GetComponent<TextMeshProUGUI>();

        coinsText.text = PlayerPrefs.GetInt("CurrentCoins", 0).ToString();
    }

    void ModifyCoins(int playerCoins)
    {
        currentCoins = playerCoins;
        coinsText.text = currentCoins.ToString();
    }
}
