using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCanvas : MonoBehaviour
{
    public Image avatar;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI shortDes;
    public TextMeshProUGUI distance;

    public void LoadData()
    {
        avatar.sprite = QuestController.instance.currentQuest.avatar;
        descriptionTxt.text = QuestController.instance.currentQuest.description;
        shortDes.text = QuestController.instance.currentQuest.customerShortText;
        //distance.text = GameController.instance.curQuest.avatar;
    }
}
