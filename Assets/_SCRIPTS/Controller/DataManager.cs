using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
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
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + QuestController.instance.totalReward);
        CanvasController.instance.ClosePanel(CanvasController.instance.modeTaxiWinPanel);
    }

    public void AddCoinAds()
    {
        PlayerPrefs.SetFloat("cash", PlayerPrefs.GetFloat("cash") + QuestController.instance.totalReward * 3);
        CanvasController.instance.ClosePanel(CanvasController.instance.modeTaxiWinPanel);
        
    }

}
