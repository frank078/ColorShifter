using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public bool isGreen = false;
    public bool isPink = false;
    public bool isBlocker = false;
    public bool isImmortality = false;

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
    public GameObject deathUI, mainMenuUI, pauseUI, highScore, shopUI; //TODO: Change after OnSceneLoaded
    public Button pauseToMenu, loseToRestart, loseToMenu, continueButton, extraCoinsButton;
    public TextMeshProUGUI highScoreText;

    public bool isRestart;

    AudioSource menuMusic;

    public UnityMonetization AdsManager;
    //true = life, false = extra coins
    bool lifeOrCoins;
    bool isCoinAd;
    bool isDeathAd;
    int maxContinues;
    // 3 = 0,1,2. Initialize to 2 so that the first death plays ad
    int LoseAd3 = 2;

    void Awake()
    {
        // SINGLETON
        // WITHOUT THIS, INSTANCE WILL BE NULL
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // will not destroy this gameObject when loading new scene
        }
        else if (Instance != null)
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
        if (curScene.buildIndex == 0 || curScene.buildIndex == 1 || curScene.buildIndex == 2) // TODO: Change index
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

            //This is High Score UI
            highScore = GameObject.Find("HighScore");

            //This is shopUI
            shopUI = GameObject.Find("ShopUI");

            //----------------------------------------------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------------------------------------------
            // TEXT
            // lose coins text
            finalCoins = GameObject.Find("CoinAmountDeath").GetComponent<Text>();

            // main menu coins text
            coinsMainMenu = GameObject.Find("CoinAmountMainMenu").GetComponent<Text>();
            coinsMainMenu.text = PlayerPrefs.GetInt("CurrentCoins", 0).ToString();

            // Death UI High Score
            highScoreText = highScore.GetComponent<TextMeshProUGUI>();

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

            continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
            continueButton.onClick.AddListener(() => ShowContinueAd());

            extraCoinsButton = GameObject.Find("ExtraCoinsButton").GetComponent<Button>();
            extraCoinsButton.onClick.AddListener(() => ShowCoinsAd());
            // ---------------------------------------------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------------------------------------------
            // Script
            AdsManager = GameObject.Find("AdsManager").GetComponent<UnityMonetization>();

            // ---------------------------------------------------------------------------------------------------------------------

            highScore.SetActive(false);
            deathUI.SetActive(false);
            pauseUI.SetActive(false);
            shopUI.SetActive(false);
            extraCoinsButton.gameObject.SetActive(false);

            SpawnPlayer(PlayerPawn);

            //TODO: SAVE SYSTEM
            coins = PlayerPrefs.GetInt("CurrentCoins", 0);
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
            SpendCoins.Instance.Coins = coins;

            ShopBehavior.Instance.GetCurrentCharacter();

            menuMusic = gameObject.GetComponent<AudioSource>();
            menuMusic.Play();

            maxContinues = 0;

            //AdsManager.instance.RequestInterstitial();
            //ResetScore(); //Only for testing, COMMENT OUT WHEN DONE TESTING
            //ResetCoins(); //Only for testing, COMMENT OUT WHEN DONE TESTING

        }
    }

    public void SpawnPlayer(GameObject selectedPlayer)
    {
        GameObject _Player;

        // if Player somehow is not null, destroy it
        if (Player != null)
        {
            Destroy(Player);
        }

        //player                          PlayerStartPos        PlayerStartRot
        _Player = Instantiate(selectedPlayer, PlayerStart.position, PlayerStart.rotation);

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

    public void ModifyScoreUI(bool isNotImmortal)
    {
        // if player is not immortal, gain point / score
        if (isNotImmortal)
        {
            score++;
            OnWallsCollided?.Invoke(score);
        }
        else
        {
            // we want the immortality to be active only ONCE after death
            SetImmortality(false);
        }
    }
    // -------------------------------------------

    public void Death()
    {
        PlayerPrefs.SetInt("CurrentCoins", coins);

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScore.SetActive(true);
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score\n" + score.ToString();
        }

        finalCoins.text = coins.ToString();
        SpendCoins.Instance.Coins = coins;

        deathUI.SetActive(true);

        ShowDeathAd();

        //ResetSubscribe();
        //ResetDifficulty();
    }

    //Reset Score ONLY FOR DEVELOPEMENT
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    // Reset Coins ONLY FOR DEVELOPEMENT
    public void ResetCoins()
    {
        PlayerPrefs.DeleteKey("CurrentCoins");
    }

    // The button to either restart or go to the main menu
    // If true = restart (remove main menu UI)
    // If false = main menu (open main menu UI)
    public void RestartOrMenuButton(bool isRestarted)
    {
        isRestart = isRestarted;
        ResetSubscribe();
        ResetDifficulty();
        Timer.isArrowPressed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetSubscribe()
    {
        OnTowerPulling = null;
        OnTowerChecking = null;
        OnCoinsCollided = null;
        OnWallsCollided = null;
    }

    public void ResetDifficulty()
    {
        SetGreen(false); // lock green colors
        SetPink(false); // lock pink colors
        SetBlocker(false); // lock blocker
        SetImmortality(false); // disable this as it will still be active if we click restart or menu when immortal is active
        score = 0;
    }

    public void SetGreen(bool value)
    {
        // true = unlocked green
        // false = locked green
        isGreen = value;
    }

    public void SetPink(bool value)
    {
        // true = unlocked pink
        // false = locked pink
        isPink = value;
    }

    public void SetBlocker(bool value)
    {
        // true = unlocked blocker
        // false = locked blocker
        isBlocker = value;
    }

    public void SetImmortality(bool value)
    {
        // true = be immortal
        // false = no immortal
        isImmortality = value;

        // Doesn't have to call here
        // 1. Can just call it on PlayerCollision Start()
        // 2. Call from here will give null ref since currentColor is set at Start where we call ChangeToImmortalColor immediately, you can get ref again but it's waste
        //if (isImmortality)
        //{
        //    GetPlayer().GetComponent<PlayerCollision>().ChangeToImmortalColor();
        //}
    }

    public void GetLatestCoins()
    {
        coinsMainMenu.text = PlayerPrefs.GetInt("CurrentCoins", 0).ToString();
        coins = PlayerPrefs.GetInt("CurrentCoins", 0);
    }

    public void ShowDeathAd()
    {
        // The death add shows up on every 3rd death
        if(LoseAd3 == 2)
        {
            AdsManager.DisplayInterstitialAd();
            LoseAd3 = -1;
            isDeathAd = true;
        }
        LoseAd3 += 1;
    }

    public void ShowContinueAd()
    {
        AdsManager.DisplayVideoAd();
        lifeOrCoins = true;
        isCoinAd = false;
        isDeathAd = false;
    }

    public void ShowCoinsAd()
    {
        AdsManager.DisplayVideoAd();
        lifeOrCoins = false;
        isCoinAd = true;
        isDeathAd = false;
    }

    public void GiveReward()
    {
        if (isDeathAd == false)
        {
            if (lifeOrCoins == true)
            {
                // extra life
                maxContinues += 1;
                Debug.Log(maxContinues);
                if (maxContinues <= 3)
                {
                    deathUI.SetActive(false);
                    // Respawn the player
                    ShopBehavior.Instance.GetCurrentCharacter();
                    Timer.isContinue = true;
                    lifeOrCoins = false;

                    SetImmortality(true);
                }

                if (maxContinues == 3)
                {
                    continueButton.gameObject.SetActive(false);
                    extraCoinsButton.gameObject.SetActive(true);
                }
            }
            else if (lifeOrCoins == false)
            {
                if (isCoinAd == true)
                {
                    //give the modafucka 20 coins
                    ModifyCoinsUI(20);
                    PlayerPrefs.SetInt("CurrentCoins", coins);
                    finalCoins.text = coins.ToString();
                    extraCoinsButton.gameObject.SetActive(false);
                    isCoinAd = false;
                }
            }
        }
    }
}
