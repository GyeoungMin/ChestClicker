using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOpenedChest : UIPopup
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button adBtn;

    void Start()
    {
        //closeBtn.onClick.AddListener(() =>
        //{
        //    ClosePopup();
        //    ADMobManager.Instance.ReloadRewardedAd();
        //});
        //adBtn.onClick.AddListener(() =>

        //{
        //    Firebase.Analytics.FirebaseAnalytics.LogEvent(
        //        "clicked_upgrade_btn",
        //        Firebase.Analytics.FirebaseAnalytics.EventPostScore,
        //        1
        //        );
        //    Firebase.Analytics.FirebaseAnalytics.LogEvent(
        //        "view_ad",
        //        Firebase.Analytics.FirebaseAnalytics.EventPostScore,
        //        1
        //        );
        //    ADMobManager.Instance.Rewarded.ShowAd();
        //});
    }
}
