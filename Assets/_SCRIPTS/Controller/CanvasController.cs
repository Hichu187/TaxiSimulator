using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MTAssets.EasyMinimapSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

    void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI damageValue;
    public float curDamage;
    [Header("Phone Canvas")]
    public GameObject phoneIcon;
    public MinimapRenderer minimap;

    //public GameObject selectBtn;
    [Header("WIN CANVAS - Mode Taxi ")]
    public GameObject modeTaxiWinPanel;
    public TextMeshProUGUI distanceUI;
    public TextMeshProUGUI qualityUI;
    public TextMeshProUGUI tipsUI;
    public TextMeshProUGUI totalRewardUI;

    [Header("WIN CANVAS - Mode Chalenge ")]
    public GameObject modeChallengeWinPanel;


    void Start()
    {
        curDamage = 100;
        EventController.instance.startGame += StartMinimap;
        EventController.instance.takeACall += TakeACall;
        EventController.instance.accepted += PickUp;
        EventController.instance.completeTrip += Complete;
        EventController.instance.damage += DamageCheck;

        minimap.gameObject.SetActive(false);
    }
    public void StartMinimap()
    {
        minimap.minimapCameraToShow = GameController.instance.player.GetComponent<MinimapCamera>();
        Invoke("OpenMinimap", 1f);
        //CarSelectionCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
    void OpenMinimap()
    {
        minimap.gameObject.SetActive(true);
    }

    public void TakeACall()
    {
        phoneIcon.GetComponent<Image>().raycastTarget = true;
        phoneIcon.transform.GetChild(1).gameObject.SetActive(true);
        phoneIcon.transform.GetChild(0).GetComponent<Animator>().CrossFade("Shake", 0);
    }

    public void PickUp()
    {
        phoneIcon.GetComponent<Image>().raycastTarget = false;
        phoneIcon.transform.GetChild(1).gameObject.SetActive(false);
        phoneIcon.transform.GetChild(0).GetComponent<Animator>().CrossFade("Normal", 0);
    }

    public void Complete()
    {
        modeTaxiWinPanel.SetActive(true);
        Time.timeScale = 0;
        distanceUI.text = QuestController.instance.distance + " km";
        qualityUI.text = QuestController.instance.quality.ToString();
        tipsUI.text = QuestController.instance.tips.ToString();
        totalRewardUI.text = (QuestController.instance.tips + QuestController.instance.cash).ToString();
    }
    public void ClosePanel(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        if (obj == modeChallengeWinPanel || obj == modeTaxiWinPanel) Time.timeScale = 1;
    }

    public void DamageCheck(float value)
    {
        float startdamage = curDamage;
        float roundedNumber = Mathf.Round(value * Mathf.Pow(10, 0)) / Mathf.Pow(10, 0);
        curDamage -= roundedNumber;

        DOVirtual.Int((int)startdamage, (int)curDamage, 0.25f, d => { damageValue.text = d + "%"; });
    }

    public void RepairVehicle()
    {
        curDamage = 100;
        damageValue.text = curDamage + "%";
        EventController.instance.Repair();
    }
}
