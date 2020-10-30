using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    string App_ID = "ca-app-pub-6546446247412058~7979527836";

    string Interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
    string Rewarded_Ad_ID = "ca-app-pub-3940256099942544/5224354917";

    private InterstitialAd loseAd;
    private RewardedAd continueRewardedAd;
    private RewardedAd extraCoinsRewardedAd;

    public static AdsManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(App_ID);

        this.continueRewardedAd = CreateAndLoadRewardedAd(Rewarded_Ad_ID);
        this.extraCoinsRewardedAd = CreateAndLoadRewardedAd(Rewarded_Ad_ID);

    }

    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.loseAd = new InterstitialAd(Interstitial_Ad_ID);

        // Called when an ad request has successfully loaded.
        this.loseAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.loseAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.loseAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.loseAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.loseAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.loseAd.LoadAd(request);
    }
    public void ShowLoseAd()
    {
        if (this.loseAd.IsLoaded())
        {
            this.loseAd.Show();
        }
    }

    public RewardedAd CreateAndLoadRewardedAd(string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    public void ShowContinueVideoAd()
    {
        if (this.continueRewardedAd.IsLoaded())
        {
            this.continueRewardedAd.Show();
        }
    }

    public void ShowCoinsVideoAd()
    {
        if (this.extraCoinsRewardedAd.IsLoaded())
        {
            this.extraCoinsRewardedAd.Show();
        }
    }

    // Interstitial Ads Events and Delegates ------------------------------------------------------------------------------------------------------
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------

    // Events and Delgates for Rewarded ads -----------------------------------------------------------------------------------------------------
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        //if (type == "Continue")
        //{
        //    GameManager.Instance.GiveExtraLife();
        //}
        //else if(type == "Coins")
        //{
        //    GameManager.Instance.GiveExtraCoins();
        //}

    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------
}
