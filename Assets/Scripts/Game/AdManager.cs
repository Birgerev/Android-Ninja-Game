using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    
    private string adUnitInterstitialId = "ca-app-pub-6282622427267773/1879202231";
    private string appId = "ca-app-pub-6282622427267773~6729926430";

    private RewardBasedVideoAd rewardBasedVideo;

    private void Start()
    {
        instance = this;

        //MobileAds.Initialize(appId);
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        loadAd();
    }

    public static void loadBanner()
    {
    }

    public void loadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.rewardBasedVideo.LoadAd(request, adUnitInterstitialId);
        rewardBasedVideo.OnAdRewarded += HandleOnAdRewarded;
    }

    public void playAd()
    {
        this.rewardBasedVideo.Show();
    }

    public bool isAdvertAvailable()
    {
        return rewardBasedVideo.IsLoaded();
    }

    public void HandleOnAdRewarded(object sender, Reward args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");

        Manager.instance.Respawn();
    }
}