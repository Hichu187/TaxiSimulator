using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle_Buy_Infomation : MonoBehaviour
{
    public GameObject buyBtn;
    public GameObject otherBtn;
    public Sprite goldStar;
    public Sprite blackStar;
    public GameObject star;
    [Header("DATA")]
    public TextMeshProUGUI nameVehicle;
    public TextMeshProUGUI topSpeed;
    public TextMeshProUGUI acc;
    public TextMeshProUGUI handling;
    public TextMeshProUGUI price;
    public int starCount;

    public void OpenInfor(VehicleData data)
    {
        transform.DOLocalMoveX(0, 0.25f);
        transform.GetChild(0).transform.DOLocalMoveX(0, 0.25f);
        otherBtn.transform.DOLocalMoveX(700, 0.25f);
        buyBtn.transform.DOLocalMoveX(0, 0.25f);
        nameVehicle.text = data.name;
        topSpeed.text = data.topSpeed.ToString();
        acc.text = data.acceleration.ToString();
        handling.text = data.handling.ToString();
        starCount = data.star;
        if (!data.isAds)
        {
            price.text = data.price.ToString();
        }
        else
        {
            price.text = data.adsPrice.ToString();
        }


        for (int i = 0; i < star.transform.childCount; i++)
        {
            if (i < starCount) star.transform.GetChild(i).GetComponent<Image>().sprite = goldStar;
            else star.transform.GetChild(i).GetComponent<Image>().sprite = blackStar;

            star.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
        }
    }

    public void CloseInfor()
    {
        transform.DOLocalMoveX(-500, 0.25f);
        transform.GetChild(0).transform.DOLocalMoveX(300, 0.25f);
        otherBtn.transform.DOLocalMoveX(0, 0.25f);
        buyBtn.transform.DOLocalMoveX(700, 0.25f);
    }
}
