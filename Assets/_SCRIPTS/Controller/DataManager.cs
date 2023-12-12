using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public TextMeshProUGUI cash;
    void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("cash")) PlayerPrefs.SetFloat("cash", 0);
        if (!PlayerPrefs.HasKey("questID")) PlayerPrefs.SetInt("questID", 0);
        if (!PlayerPrefs.HasKey("challengeId")) PlayerPrefs.SetInt("challengeId", 0);
        if (!PlayerPrefs.HasKey("questTime")) PlayerPrefs.SetInt("questTime", 0);


    }
    void Start()
    {
        EventController.instance.completeTrip += CompleteQuest;
        EventController.instance.addCash += AddCoinValue;

        cash.text = PlayerPrefs.GetFloat("cash").ToString();
    }

    void CompleteQuest()
    {


        if (PlayerPrefs.GetInt("questID") >= 7)
        {
            PlayerPrefs.SetInt("questID", 0);
        }
        else
        {
            PlayerPrefs.SetInt("questID", PlayerPrefs.GetInt("questID") + 1);
        }
    }

    public void AddCoinNoAds()
    {
        int cashvalue = (int)PlayerPrefs.GetFloat("cash");
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + QuestController.instance.totalReward);
        CanvasController.instance.ClosePanel(CanvasController.instance.modeTaxiWinPanel);

        DOVirtual.Int(cashvalue, (int)PlayerPrefs.GetFloat("cash"), 0.75f, c => cash.text = c.ToString());
    }

    public void AddCoinAds()
    {
        int cashvalue = (int)PlayerPrefs.GetFloat("cash");
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + QuestController.instance.totalReward * 3);
        CanvasController.instance.ClosePanel(CanvasController.instance.modeTaxiWinPanel);

        DOVirtual.Int(cashvalue, (int)PlayerPrefs.GetFloat("cash"), 0.75f, c => cash.text = c.ToString());
    }

    public void AddCoinAdsCommon()
    {
        int cashvalue = (int)PlayerPrefs.GetFloat("cash");
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + 500);
        DOVirtual.Int(cashvalue, (int)PlayerPrefs.GetFloat("cash"), 0.75f, c => cash.text = c.ToString());
    }
    public void AddCoinValue(int value)
    {
        int cashvalue = (int)PlayerPrefs.GetFloat("cash");
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + value);

        DOVirtual.Int(cashvalue, cashvalue + value, 0.75f, c => cash.text = c.ToString());
    }

}
