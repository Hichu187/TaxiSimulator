using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ADSController : MonoBehaviour
{
     public static _ADSController instance;
    private void Awake()
    {
        instance = this;

    }
    public bool isAdsActive;

    private void Start()
    {

        OffMREC();
        BannerAds();

    }
    public void AppOpenAds()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowAppOpen();
        }
    }

    public void BannerAds()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowBanner(Bounce.AdsViewPosition.BottomCenter);
        }

    }
    public void setIsShowAds(bool isShow)
    {
        isAdsActive = isShow;
    }
    public bool getIsShowAds()
    {
        return isAdsActive;
    }

    public void ButtonShowInter(GameObject obj)
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowInterstitial(obj.name);
            Bounce.BounceAdsSdk.HideMREC();
        }
    }

    public void ShowMREC(Bounce.AdsViewPosition adsMRECpos)
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowMREC(adsMRECpos);
            Bounce.BounceAdsSdk.HideBanner();
        }
    }
    public void OffMREC()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.HideMREC();
            Bounce.BounceAdsSdk.ShowBanner(Bounce.AdsViewPosition.BottomCenter);
        }
    }
    public void RewardWin()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {
                   

                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_rewardWin", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_rewardWin", "log", 1);
        }
    }

    public void RewardBuySword()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {
                    

                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_RewardSword", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_RewardSword", "log", 1);
        }
    }

    public void RewardBuyKey()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {
                    PlayerPrefs.SetInt("key", 3);
                }
                   
            }, "btn_RewardBuyKey", "log", 1);
        }
    }

    public void Restart1vs1()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {

                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_RestartTour", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_RestartTour", "log", 1);
        }
    }


    public void RewardClaimProgressSword(int i)
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {
                    PlayerPrefs.SetString("swordPurchased", PlayerPrefs.GetString("swordPurchased") + i + "/");
                    PlayerPrefs.SetInt("equipedSword", i);
                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_ClaimWinSword", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_ClaimWinSword", "log", 1);
        }
    }

    public void OpenWinChest()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {

                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_ClaimWinChest", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_ClaimWinChest", "log", 1);
        }
    }

    public void AdsJoinTournament()
    {
        if (isAdsActive)
        {
            Bounce.BounceAdsSdk.ShowRewarded((res) =>
            {
                if (res)
                {

                }
                Bounce.BounceSdk.LogAds(Bounce.GameLoggerAdsType.REWARDED, Bounce.GameLoggerAdsState.SHOW, "btn_joinTournament", "level", PlayerPrefs.GetInt("questID"));
            }, "btn_joinTournament", "log", 1);
        }
    }
}
