using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehavior : MonoBehaviour
{
    #region SIngleton:ShopBehavior

    public static ShopBehavior Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    // Variables
    [System.Serializable] class ShopItem
    {
        public Mesh ItemObject;
        public GameObject playerGameObject;
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

    bool[] buttonsBought;

    // Start is called before the first frame update
    void Start()
    {
        //For testing only!
        //ES3.DeleteKey("Asuna");

        itemTemplate = shopScrollView.GetChild(0).gameObject;

        buttonsBought = new bool[ShopItemsList.Count];

        buttonsBought = ES3.Load("Asuna", buttonsBought);

        // remember to also change the constraint count in the content scroll
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            // the shop item gameobject
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<MeshFilter>().mesh = ShopItemsList[i].ItemObject;
            g.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
            g.transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(450, 450, 450);
            g.transform.GetChild(2).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            //Buy button
            buyButton = g.transform.GetChild(3).GetComponent<Button>();
            buyButton.interactable = !ShopItemsList[i].IsPurchased;
            buyButton.AddEventListener(i, OnShopItemButtonClicked);
            //Select Button
            selectButton = g.transform.GetChild(4).GetComponent<Button>();
            selectButton.interactable = !ShopItemsList[i].IsPurchased;
            selectButton.AddEventListener(i, OnSelectButtonClicked);

            // take the save for bought buttons
            if (buttonsBought[i] == true)
            {
                ShopItemsList[i].IsPurchased = true;

                //Disable the buy button
                buyButton.gameObject.SetActive(false);

                //Enable the select Button
                selectButton.gameObject.SetActive(true);
            }          
        }

        // First button always bought --------------------------------------------------------------
        ShopItemsList[1].IsPurchased = true;

        //Disable the buy button
        buyButton = shopScrollView.GetChild(1).GetChild(3).GetComponent<Button>();
        buyButton.gameObject.SetActive(false);

        //Enable the select Button
        selectButton = shopScrollView.GetChild(1).GetChild(4).GetComponent<Button>();
        selectButton.gameObject.SetActive(true);
        //------------------------------------------------------------------------------------------

        // REMINDER: The items on ShopScrollView start at 1
        Destroy(itemTemplate);

        if (PlayerPrefs.GetInt("CurrentChar", 0) == 0)
        {
            FirstButtonToSelect();
        }

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

            buttonsBought[itemIndex] = true;
            ES3.Save("Asuna", buttonsBought);

            SetCoinsUI();
        }
        else
        {
            Debug.Log("Broke ass bitch");
        }
    }

    void OnSelectButtonClicked(int itemIndex)
    {
        //This loop resets other buttons selected
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            selectButton = shopScrollView.GetChild(i).GetChild(4).GetComponent<Button>();
            selectButton.interactable = true;
            selectButton.transform.GetChild(0).GetComponent<Text>().text = "SELECT";
        }
        // This gets the index of the item selected
        selectButton = shopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();
        selectButton.interactable = false;
        selectButton.transform.GetChild(0).GetComponent<Text>().text = "SELECTED";

        PlayerPrefs.SetInt("CurrentChar", itemIndex);

        //Switch the player character
        GameManager.Instance.SpawnPlayer(ShopItemsList[itemIndex].playerGameObject);
    }

    void SetCoinsUI()
    {
        CoinsText.text = SpendCoins.Instance.Coins.ToString();
    }

    // Call on game manager OnSceneLoaded
    public void GetCurrentCharacter()
    {
        int _PlayerIndex;

        _PlayerIndex = PlayerPrefs.GetInt("CurrentChar", 0);

        //Switch the player character
        GameManager.Instance.SpawnPlayer(ShopItemsList[_PlayerIndex].playerGameObject);
    }

    void FirstButtonToSelect()
    {
        // First Button does not need buy
        ShopItemsList[1].IsPurchased = true;

        buyButton = shopScrollView.GetChild(1).GetChild(3).GetComponent<Button>();
        buyButton.gameObject.SetActive(false);

        selectButton = shopScrollView.GetChild(1).GetChild(4).GetComponent<Button>();
        selectButton.gameObject.SetActive(true);
        selectButton.interactable = false;
        selectButton.transform.GetChild(0).GetComponent<Text>().text = "SELECTED";

        //OnSelectButtonClicked(1); //TODO: Fix the +1 error every time you start the shop
    }
}
