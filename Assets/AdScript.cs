﻿using UnityEngine;
using System.Collections;

public class AdScript : MonoBehaviour
{
    /*
    bool hasShownAdOneTime;

    // Use this for initialization
    void Start()
    {
        //Request Ad
        //RequestInterstitialAds();
    }

    public void showInterstitialAd()
    {
        //Show Ad

        interstitial.Show();

            //Stop Sound
            //

            Debug.Log("SHOW AD XXX");

    }

    InterstitialAd interstitial;
    public void RequestInterstitialAds()
    {
        string adID = "ca-app-pub-6282622427267773/1879202231";

#if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        //***Test***
        AdRequest request = new AdRequest.Builder()
       .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
       .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
       .Build();


        //***Production***
        //AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        
        interstitial.OnAdLoaded += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        Debug.Log("AD LOADED XXX");

    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        //Resume Play Sound
        //if (interstitial.IsLoaded())
            GameObject.Find("UI").SetActive(false);

    }*/
}