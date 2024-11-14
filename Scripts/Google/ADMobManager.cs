using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADMobManager
{
    public static readonly ADMobManager Instance = new ADMobManager();

    public ADMobBanner Banner { get { return ADMobBanner.Instance; } }
    public ADMobReward Rewarded { get { return ADMobReward.Instance; } }

    public void Init()
    {
        Debug.Log("ADMobManager :: Init()");
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            Debug.Log($"MobileAds Initialize : {initStatus}");
            LoadAds();
        });
    }

    public void LoadAds()
    {
        Banner.LoadAd();
        Rewarded.LoadAd();
    }

    public void ReloadRewardedAd()
    {
        Rewarded.LoadAd();
    }

    public void DestroyAds()
    {
        Banner.DestroyAd();
        Rewarded.DestroyAd();
    }
}
