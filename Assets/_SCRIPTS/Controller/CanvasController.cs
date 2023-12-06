using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MTAssets.EasyMinimapSystem;
using Unity.VisualScripting;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

    void Awake()
    {
        instance = this;
    }
    [Header("Phone Canvas")]
    public MinimapRenderer minimap;
    public GameObject CarSelectionCanvas;

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
        EventController.instance.startGame += StartMinimap;

        EventController.instance.completeTrip += Complete;

        minimap.gameObject.SetActive(false);
    }
    public void StartMinimap()
    {
        minimap.minimapCameraToShow = GameController.instance.player.GetComponent<MinimapCamera>();
        Invoke("OpenMinimap", 1f);
        CarSelectionCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
    void OpenMinimap()
    {
        minimap.gameObject.SetActive(true);

    }
    public void Complete()
    {
        modeTaxiWinPanel.SetActive(true);
        Time.timeScale = 0;
        distanceUI.text = QuestController.instance.distance + " km" ;
        qualityUI.text = QuestController.instance.quality.ToString();
        tipsUI.text = QuestController.instance.tips.ToString();
        totalRewardUI.text = QuestController.instance.totalReward.ToString();
    }
    public void ClosePanel(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        if (obj == modeChallengeWinPanel || obj == modeTaxiWinPanel) Time.timeScale = 1;
    }
}
