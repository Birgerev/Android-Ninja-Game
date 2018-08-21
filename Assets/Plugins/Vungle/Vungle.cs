using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Threading;

#pragma warning disable 618
#pragma warning disable 162
#pragma warning disable 67

public class Vungle
{
	//Change this constant fields when a new version of the plugin or sdk was released
	private const string PLUGIN_VERSION = "5.4.1";
	private const string IOS_SDK_VERSION = "5.4.0";
	private const string WIN_SDK_VERSION = "5.3.2";
	private const string ANDROID_SDK_VERSION = "5.3.2";

	#region Events

	// Fired when a Vungle ad starts
	public static event Action<string> onAdStartedEvent;
	
	//Fired when a Vungle ad finished and provides the entire information about this event.
	public static event Action<string, AdFinishedEventArgs> onAdFinishedEvent; 

	// Fired when a Vungle ad is ready to be displayed
	public static event Action<string, bool> adPlayableEvent;
	
	// Fired when a Vungle SDK initialized
	public static event Action onInitializeEvent;
	
	// Fired log event from sdk.
	public static event Action<string> onLogEvent;

	public static event Action<string, string> onPlacementPreparedEvent;
	public static event Action<string, string> onVungleCreativeEvent;

#if (!VUNGLE_AD_OFF)
	static void adStarted(string placementID)
	{
		if( onAdStartedEvent != null )
			#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			VungleSceneLoom.Loom.QueueOnMainThread(() =>
				{
					onAdStartedEvent(placementID);
				});
			#else
			onAdStartedEvent(placementID);
			#endif
	}

	static void adPlayable(string placementID, bool playable)
	{
		if( adPlayableEvent != null )
			#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			VungleSceneLoom.Loom.QueueOnMainThread(() =>
				{
					adPlayableEvent(placementID, playable);
				});
			#else
			adPlayableEvent(placementID, playable);
			#endif
	}
	
	static void onLog(string log)
	{
		if( onLogEvent != null )
			#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			VungleSceneLoom.Loom.QueueOnMainThread(() =>
				{
					onLogEvent(log);
				});
			#else
			onLogEvent(log);
			#endif
	}

	static void onPlacementPrepared(string placementID, string bidToken)
	{
		if( onPlacementPreparedEvent != null )
			onPlacementPreparedEvent(placementID, bidToken);
	}

	static void onVungleCreative(string placementID, string creativeID)
	{
		if( onVungleCreativeEvent != null )
			onVungleCreativeEvent(placementID, creativeID);
	}

	static void adFinished(string placementID, AdFinishedEventArgs args)
	{
		if (onAdFinishedEvent != null)
			#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			VungleSceneLoom.Loom.QueueOnMainThread(() =>
				{
					onAdFinishedEvent(placementID, args);
				});
			#else
			onAdFinishedEvent(placementID, args);
			#endif
	}

	static void onInitialize()
	{
		if (onInitializeEvent != null)
			#if UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			VungleSceneLoom.Loom.QueueOnMainThread(() =>
				{
					onInitializeEvent();
				});
			#else
			onInitializeEvent();
			#endif
	}
#endif

	#endregion

	public static string VersionInfo
	{
		get
		{
#if (!VUNGLE_AD_OFF)
			StringBuilder stringBuilder = new StringBuilder("unity-");
			#if UNITY_IPHONE
			return stringBuilder.Append(PLUGIN_VERSION).Append("/iOS-").Append(IOS_SDK_VERSION).ToString();
			#elif UNITY_ANDROID
			return stringBuilder.Append(PLUGIN_VERSION).Append("/android-").Append(ANDROID_SDK_VERSION).ToString();
			#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
			return stringBuilder.Append(PLUGIN_VERSION).Append("/windows-").Append(WIN_SDK_VERSION).ToString();
			#else
			return stringBuilder.Append(PLUGIN_VERSION).ToString();
			#endif
#else
			return "OFF";
#endif
		}
	}

	static Vungle()
	{
#if (!VUNGLE_AD_OFF)
		VungleManager.OnAdStartEvent += adStarted;
		VungleManager.OnAdFinishedEvent += adFinished;
		VungleManager.OnAdPlayableEvent += adPlayable;
		VungleManager.OnSDKLogEvent += onLog;
		VungleManager.OnSDKInitializeEvent += onInitialize;
		VungleManager.OnPlacementPreparedEvent += onPlacementPrepared;
		VungleManager.OnVungleCreativeEvent += onVungleCreative;
#endif
	}

	// Initializes the Vungle SDK. Pass in your Android and iOS app ID's from the Vungle web portal.
	public static void init(string appId, string[] placements)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
        return;
#endif
#if UNITY_IPHONE
		VungleBinding.startWithAppId(appId, placements, PLUGIN_VERSION);
#elif UNITY_ANDROID
		VungleAndroid.init(appId, placements, PLUGIN_VERSION);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        VungleWin.init(appId, PLUGIN_VERSION, placements);
		VungleSceneLoom.Initialize();
#endif
#endif
    }


	// Initializes the Vungle SDK. Pass in your Android and iOS app ID's from the Vungle web portal.
	public static void init(string appId, string[] placements, bool initHeaderBiddingDelegate)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
        return;
#endif
#if UNITY_IPHONE
		VungleBinding.startWithAppId(appId, placements, PLUGIN_VERSION, initHeaderBiddingDelegate);
#elif UNITY_ANDROID
		VungleAndroid.init(appId, placements, PLUGIN_VERSION);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        VungleWin.init(appId, PLUGIN_VERSION, placements);
		VungleSceneLoom.Initialize();
#endif
#endif
	}
    
    // Sets if sound should be enabled or not
    public static void setSoundEnabled(bool isEnabled)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return;
#endif
#if UNITY_IPHONE
		VungleBinding.setSoundEnabled(isEnabled);
#elif UNITY_ANDROID
		VungleAndroid.setSoundEnabled(isEnabled);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        VungleWin.setSoundEnabled(isEnabled);
#endif
#endif
	}


	// Checks to see if a video is available
	public static bool isAdvertAvailable(string placementID)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return false;
#endif
#if UNITY_IPHONE
		return VungleBinding.isAdAvailable(placementID);
#elif UNITY_ANDROID
		return VungleAndroid.isVideoAvailable(placementID);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        return VungleWin.isVideoAvailable(placementID);
#else
		return false;
#endif
#else
		return false;
#endif
	}

	// Load ads with placementID.
	public static void loadAd(string placementID)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return;
#endif
#if UNITY_IPHONE
		VungleBinding.loadAd(placementID);
#elif UNITY_ANDROID
		VungleAndroid.loadAd(placementID);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
		VungleWin.loadAd( placementID );
#endif
#endif
	}

	// Close flex ads.
	public static bool closeAd(string placementID)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return false;
#endif
#if UNITY_IPHONE
		return VungleBinding.closeAd(placementID);
#elif UNITY_ANDROID
		return VungleAndroid.closeAd(placementID);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
		return false;
#endif
#endif
	}

	// Displays an ad with the no options. The user option is only supported for incentivized ads.
	public static void playAd(string placementID)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return;
#endif
#if UNITY_IPHONE
		VungleBinding.playAd(placementID);
#elif UNITY_ANDROID
		VungleAndroid.playAd(placementID);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
		VungleWin.playAd(placementID );
#endif
#endif
	}
	
	// Displays an ad with the given options. The user option is only supported for incentivized ads.
	public static void playAd(Dictionary<string,object> options, string placementID)
	{
#if (!VUNGLE_AD_OFF)
#if UNITY_EDITOR
		return;
#endif
		if(options == null) {
			options = new Dictionary<string, object>();
		}
#if UNITY_IPHONE
		VungleBinding.playAd(options, placementID);
#elif UNITY_ANDROID
		VungleAndroid.playAd(options, placementID);
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
        VungleWin.playAd( options, placementID );
#endif
#endif
	}
	
	// Clear sleep
	public static void clearSleep()
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return;
		#endif
		#if UNITY_IPHONE
		VungleBinding.clearSleep();
		#elif UNITY_ANDROID
		#else
		#endif
#endif
	}
	
	public static void setEndPoint(string endPoint)
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return;
		#endif
		#if UNITY_IPHONE
		VungleBinding.setEndPoint(endPoint);
		#elif UNITY_ANDROID
		#else
		return;
		#endif
#endif
	}

	public static void setLogEnable(bool enable)
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return;
		#endif
		#if UNITY_IPHONE
		VungleBinding.enableLogging(enable);
		#elif UNITY_ANDROID
		#else
		return;
		#endif
#endif
	}
	
	public static string getEndPoint()
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return "";
		#endif
		#if UNITY_IPHONE
		return VungleBinding.getEndPoint();
		#elif UNITY_ANDROID
		return "";
		#else
		return "";
		#endif
#else
		return "";
#endif
	}
	
	public static void onResume()
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return;
		#endif
		#if UNITY_ANDROID
		VungleAndroid.onResume();
		#endif
#endif
	}

	public static void onPause()
	{
#if (!VUNGLE_AD_OFF)
		#if UNITY_EDITOR
		return;
		#endif
		#if UNITY_ANDROID
		VungleAndroid.onPause();
		#endif
#endif
	}
}
