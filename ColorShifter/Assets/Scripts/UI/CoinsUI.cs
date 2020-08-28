using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // call textmeshpro

public class CoinsUI : MonoBehaviour
{
    private TextMeshProUGUI coinsText;

    void Start()
    {
        GameManager.Instance.OnCoinsCollided += ModifyCoins;

        coinsText = gameObject.GetComponent<TextMeshProUGUI>();

        coinsText.text = "0"; //TODO: SAVE SYSTEM LOAD COINS
    }


    void ModifyCoins(int playerCoins)
    {
        int currentCoins = playerCoins;

        coinsText.text = currentCoins.ToString();
    }
}
