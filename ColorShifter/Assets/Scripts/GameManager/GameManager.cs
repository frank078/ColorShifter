using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene curScene;

    public static GameManager Instance { get; private set; } // to call the gamemananger

    [SerializeField] GameObject PlayerPawn = null; // used for the player to spawned

    GameObject Player = null; // after the player spawned it will set the player to this (Use this for ref player)

    public GameObject GetPlayer() { return Player; } // get player

    [SerializeField] Transform PlayerStart = null;

    public MulticastOneParam OnTowerPulling; // subscribed from WallManager
    public MulticastOneParam OnTowerChecking; // subscribed from WallManager
    public MulticastOneParam OnCoinsCollided; // subscribed from CoinsUI
    public MulticastOneParam OnWallsCollided; // subscribed from ScoreUI

    // SCORE & COINS
    private int m_score = 0;
    private int m_coins = 0;

    // DYNAMIC DIFFICULTY
    public bool isBlue = false;

    public int score // GET SET the value
    {
        get { return m_score; }
        set { m_score = value; }
    }
    public int coins // GET SET the value
    {
        get { return m_coins; }
        set { m_coins = value; }
    }
    //-------------------

    public Text finalCoins, coinsMainMenu;
    public GameObject deathUI, mainMenuUI, pauseUI; //TODO: Change after OnSceneLoaded
    public Button pauseToMenu, loseToRestart, loseToMenu;

    public bool isRestart;

    void Awake()
    {
        // SINGLETON
        // WITHOUT THIS, INSTANCE WILL BE NULL
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // will not destroy this gameObject when loading new scene
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // ------------------------------------------
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        curScene = scene;
        // TowerScene and DickyScene
        if(curScene.buildIndex == 0 || curScene.buildIndex == 1) // TODO: Change index
        {
            Debug.Log("This is game scene");

            PlayerStart = GameObject.Find("PlayerStart").transform;

            //----------------------------------------------------------------------------------------------------------------------
            // GAMEOBJECT 
            // This is death UI
            deathUI = GameObject.Find("LoseUI");

            //This is main menu UI
            mainMenuUI = GameObject.Find("MainMenu");

            //This is pause UI
            pauseUI = GameObject.Find("PauseUI");

            //----------------------------------------------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------------------------------------------
            // TEXT
            // lose coins text
            finalCoins = GameObject.Find("CoinAmountDeath").GetComponent<Text>();

            // main menu coins text
            coinsMainMenu = GameObject.Find("CoinAmountMainMenu").GetComponent<Text>();
            coinsMainMenu.text = "0"; // TODO: Once save system is in, change the amount to the saved coins

            // ---------------------------------------------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------------------------------------------
            // BUTTONS
            // Pause UI's Main Menu button
            pauseToMenu = GameObject.Find("PauseMenuButton").GetComponent<Button>();
            pauseToMenu.onClick.AddListener(() => RestartOrMenuButton(false));

            // Lose  UI's Main Menu button
            loseToMenu = GameObject.Find("LoseMenuButton").GetComponent<Button>();
            loseToMenu.onClick.AddListener(() => RestartOrMenuButton(false));

            // Lose UI's Restart Button
            loseToRestart = GameObject.Find("RestartButton").GetComponent<Button>();
            loseToRestart.onClick.AddListener(() => RestartOrMenuButton(true));
            // ---------------------------------------------------------------------------------------------------------------------

            deathUI.SetActive(false);
            pauseUI.SetActive(false);

            SpawnPlayer();

            //TODO: SAVE SYSTEM
            coins = 0;
            score = 0;
        }
    }

    void SpawnPlayer()
    {
        GameObject _Player;

        //player                          PlayerStartPos        PlayerStartRot
        _Player = Instantiate(PlayerPawn, PlayerStart.position, PlayerStart.rotation);

        Player = _Player;
    }

    // DELEGATES

    // This way it calls all the ColoredWalls. If you want one of them, has to ref the selected walls
    public void ModifyColoredWalls(int TowerNumber)
    {
        OnTowerPulling?.Invoke(TowerNumber); // call the delegate functions that was subscribed
    }

    // This way it calls all the ColoredWalls. If you want one of them, has to ref the selected walls
    public void CheckPlayerColor(int NextTowerNumber)
    {
        OnTowerChecking?.Invoke(NextTowerNumber); // call the delegate functions that was subscribed
    }

    public void ModifyCoinsUI(int coinsAmount)
    {
        coins = coins + coinsAmount;
        OnCoinsCollided?.Invoke(coins);
    }

    public void ModifyScoreUI()
    {
        score++;
        OnWallsCollided?.Invoke(score);
    }
    // -------------------------------------------

    public void Death()
    {
        finalCoins.text = coins.ToString();

        deathUI.SetActive(true);

        ResetSubscribe();
    }

    // The button to either restart or go to the main menu
    // If true = restart (remove main menu UI)
    // If false = main menu (open main menu UI)
    public void RestartOrMenuButton(bool isRestarted)
    {
        isRestart = isRestarted;
        ResetSubscribe();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetSubscribe()
    {
        OnTowerPulling = null;
        OnTowerChecking = null;
        OnCoinsCollided = null;
        OnWallsCollided = null;
    }

    public void SetBlue()
    {
        isBlue = true;
    }
}
