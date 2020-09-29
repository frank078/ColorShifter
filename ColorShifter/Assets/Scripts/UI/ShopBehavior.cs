﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    // Variables
    [System.Serializable] class ShopItem
    {
        public Mesh ItemObject;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Text CoinsText;

    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform shopScrollView;

    Button buyButton;
    Button selectButton;

    // Start is called before the first frame update
    void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        // remember to also change the constraint count in the content scroll
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<MeshFilter>().mesh = ShopItemsList[i].ItemObject;
            g.transform.GetChild(2).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            buyButton = g.transform.GetChild(3).GetComponent<Button>();
            buyButton.interactable = !ShopItemsList[i].IsPurchased;
            buyButton.AddEventListener(i, OnShopItemButtonClicked);
            selectButton = g.transform.GetChild(4).GetComponent<Button>();
            selectButton.interactable = !ShopItemsList[i].IsPurchased;
            selectButton.AddEventListener(i, OnSelectButtonClicked);
        }

        Destroy(itemTemplate);

        // Set the coins in the UI
        SetCoinsUI();
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        if (SpendCoins.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            SpendCoins.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            //Purchase Item
            ShopItemsList[itemIndex].IsPurchased = true;

            //Disable the buy button
            buyButton = shopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            buyButton.gameObject.SetActive(false);
            //buyButton.interactable = false;
            //buyButton.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";

            //Enable the select Button
            selectButton = shopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
            selectButton.gameObject.SetActive(true);
            // Set the coins in the UI
            SetCoinsUI();
        }
        else
        {
            Debug.Log("Broke ass bitch");
        }
    }

    void OnSelectButtonClicked(int itemIndex)
    {
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            selectButton = shopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
            selectButton.interactable = true;
            selectButton.transform.GetChild(0).GetComponent<Text>().text = "SELECT";
        }
        selectButton = shopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
        selectButton.interactable = false;
        selectButton.transform.GetChild(0).GetComponent<Text>().text = "SELECTED";
    }

    void SetCoinsUI()
    {
        CoinsText.text = SpendCoins.Instance.Coins.ToString();
    }
}
