using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MTAssets.EasyMinimapSystem;
using Unity.VisualScripting;

public class CanvasController : MonoBehaviour
{
    public PhoneCanvas phonePanel;
    public MinimapRenderer minimap;
    public GameObject CarSelectionCanvas;
    //public GameObject selectBtn;
    [Header("WIN CANVAS ")]
    public GameObject modeTaxiWinPanel;
    public GameObject modeChallengeWinPanel;

    void Start()
    {
        EventController.instance.startGame += StartMinimap;
        EventController.instance.accepted += ClosePhoneNotice;
        EventController.instance.refuse += ClosePhoneNotice;
        EventController.instance.takeACall += OpenPhoneNotice;
        EventController.instance.completeTrip += Complete;

        minimap.gameObject.SetActive(false);
    }
    public void StartMinimap()
    {
        minimap.minimapCameraToShow = GameController.instance.player.GetComponent<MinimapCamera>();
        Invoke("OpenMinimap",1f);
        //selectBtn.SetActive(true);
        CarSelectionCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
    void OpenMinimap()
    {
        minimap.gameObject.SetActive(true);
    }
    public void OpenPhoneNotice()
    {
        phonePanel.LoadData();
        phonePanel.gameObject.SetActive(true);
        phonePanel.transform.DOMoveY(0, 0.25f)
        .SetEase(Ease.Linear);
    }

    public void ClosePhoneNotice()
    {
        phonePanel.transform.DOMoveY(-1000, 0.5f)
        .OnComplete(() => { phonePanel.gameObject.SetActive(false); })
        .SetEase(Ease.Linear);
    }

    public void Complete()
    {
        modeTaxiWinPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePanel(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        if (obj == modeChallengeWinPanel || obj == modeTaxiWinPanel) Time.timeScale = 1;
    }
}
