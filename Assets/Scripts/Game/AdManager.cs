using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    string appID = "";
    string iosAppID = "ios_app_id";
    string androidAppID = "5b78528a1c1d32055a2bdd06";
    string windowsAppID = "windows_app_id";

    string[] placements = new string[] { "REVIVE-6595526" };

    void Start()
    {
        instance = this;

        appID = androidAppID;
        Vungle.init(androidAppID, placements);
        loadAd();
    }

    public void playAd()
    {
        //Will Throw an error on PC
        try { 
            Vungle.playAd(placements[0]);

            Vungle.onAdFinishedEvent += (placementID, args) => {
                if(args.IsCompletedView)
                    HandleOnAdRewarded();
            };
        }
        catch (System.Exception e)
        {

        }
    }

    public void loadAd()
    {
        //Will Throw an error on PC
        try
        {
            Vungle.loadAd(placements[0]);
        }
        catch (System.Exception e)
        {

        }
        //Vungle.playAdWithOptions(new Dictionary<string, object>() { { "orientation", 6 } });
    }

    public bool isAdvertAvailable()
    {
        //Will Throw an error on PC
        if(Application.isMobilePlatform )
            return Vungle.isAdvertAvailable(placements[0]);
        return true;
    }

    public void HandleOnAdRewarded()
    {
        MonoBehaviour.print("HandleAdLoaded event received");

        Manager.instance.Respawn();
    }
}
