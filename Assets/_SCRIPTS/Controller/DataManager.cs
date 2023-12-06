using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("questID")) PlayerPrefs.SetInt("questID", 0);
        if (!PlayerPrefs.HasKey("parkingModeID")) PlayerPrefs.SetInt("parkingModeID", 0);


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
}
