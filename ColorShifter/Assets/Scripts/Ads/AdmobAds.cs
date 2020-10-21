using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class AdmobAds : MonoBehaviour
{
    string GameID = "";

    // Test IDS
    string InterstitialAdID = "ca-app-pub-3940256099942544/1033173712";
    string rewarded_Ad_ID = "ca-app-pub-3940256099942544/5224354917";

    //My IDs
    // Make sure these are grayed out unless ready to ship, these are the real ads mothafucka
    string ContinueRewardAdID = "ca-app-pub-6546446247412058/7301920732";
    string CoinsRewardAdID = "ca-app-pub-6546446247412058/4288528982";
    string LoseAdID = "ca-app-pub-6546446247412058/5322586669";

    public InterstitialAd interstitial;
    public RewardBasedVideoAd ContinueAd;
    public RewardBasedVideoAd CoinsAd;

    public static AdmobAds instance;

    private void Awake()
    {
        //Optional 
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        ContinueAd = RewardBasedVideoAd.Instance;
        CoinsAd = RewardBasedVideoAd.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(GameID);

    }

    #region rewarded Video Ads

    public void loadContinueVideo()
    {
        ContinueAd.LoadAd(new AdRequest.Builder().Build(), rewarded_Ad_ID);

        ContinueAd.OnAdLoaded += HandleRewardedAdLoaded;
        ContinueAd.OnAdClosed += HandleRewardedAdClosed;
        ContinueAd.OnAdOpening += HandleRewardedAdOpening;
        ContinueAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        ContinueAd.OnAdRewarded += ContinueReward;
        ContinueAd.OnAdLeavingApplication += HandleOnRewardAdleavingApp;

    }

    public void loadCoinsVideo()
    {
        CoinsAd.LoadAd(new AdRequest.Builder().Build(), rewarded_Ad_ID);

        CoinsAd.OnAdLoaded += HandleRewardedAdLoaded;
        CoinsAd.OnAdClosed += HandleRewardedAdClosed;
        CoinsAd.OnAdOpening += HandleRewardedAdOpening;
        CoinsAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        CoinsAd.OnAdRewarded += CoinsReward;
        CoinsAd.OnAdLeavingApplication += HandleOnRewardAdleavingApp;

    }

    /// rewarded video events //////////////////////////////////////////////

    public event EventHandler<EventArgs> OnAdLoaded;

    public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

    public event EventHandler<EventArgs> OnAdOpening;

    public event EventHandler<EventArgs> OnAdStarted;

    public event EventHandler<EventArgs> OnAdClosed;

    public event EventHandler<Reward> OnAdRewarded;

    public event EventHandler<EventArgs> OnAdLeavingApplication;

    public event EventHandler<EventArgs> OnAdCompleted;

    /// Rewared events //////////////////////////



    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Video Loaded");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Video not loaded");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Video Loading");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("Video Loading failed");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Video Loading failed");
    }

    public void ContinueReward(object sender, Reward args)
    {
        GameManager.Instance.ContinueRward();
    }

    public void CoinsReward(object sender, Reward args)
    {
        GameManager.Instance.CoinsReward();
    }

    public void HandleOnRewardAdleavingApp(object sender, EventArgs args)
    {
        Debug.Log("when user clicks the video and open a new window");
    }



    public void ShowContinueVideoAd()
    {
        if (ContinueAd.IsLoaded())
        {
            ContinueAd.Show();
        }
        else
        {
            Debug.Log("Rewarded Video ad not loaded");
        }
    }

    public void ShowCoinsVideoAd()
    {
        if (CoinsAd.IsLoaded())
        {
            CoinsAd.Show();
        }
        else
        {
            Debug.Log("Rewarded Video ad not loaded");
        }
    }

    #endregion

    #region interstitial

    public void requestInterstital()
    {
        this.interstitial = new InterstitialAd(InterstitialAdID);

        this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.interstitial.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.interstitial.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    #endregion

    #region adDelegates

    //Delegates that i dont know
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad Loaded");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("couldnt load ad" + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Ad Closed");
        //requestInterstital(); // Optional : in case you want to load another interstial ad rightaway
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

}