using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MTAssets.EasyMinimapSystem;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController instance;
    void Awake() { instance = this; }
    [Header("GENERAL DATA")]
    [SerializeField] List<QuestData> listQuests;
    public List<Transform> customerPositions;
    public List<Transform> destinationPositions;
    public List<Transform> questPositions;
    private float startTips = 100;
    [Header("DATA")]
    [SerializeField] float timer;
    [SerializeField] MinimapRoutes mapRoutes;
    float countDown;
    int currentQuestId;
    public QuestData currentQuest;
    public Transform curDestination;

    public bool isPickedUpCustomer = false;
    public GameObject routes;
    private LineRenderer line;

    //Complete data
    public float distance;
    public float cash;
    public float tips;
    public RideQuality quality;
    public float totalReward;

    [Header("Phone Canvas")]
    public PhoneCanvas phonePanel;
    public TextMeshProUGUI distanceUI;
    public TextMeshProUGUI cashUI;
    public GameObject CarSelectionCanvas;

    void Start()
    {
        //event
        EventController.instance.accepted += SetUp_Start_Destination_Position;
        EventController.instance.pickUpCustomer += SetDestinationPoint;
        EventController.instance.completeTrip += Complete;
        EventController.instance.startGame += CountDownTimer;
        //CountDownTimer();
        EventController.instance.refuse += CountDownTimer;
        EventController.instance.completeTrip += CountDownTimer;

        //
        SetUp();
    }

    void SetUp()
    {
        foreach (var c in customerPositions) { c.gameObject.SetActive(false); }
        foreach (var d in destinationPositions) { d.gameObject.SetActive(false); }
        foreach (var q in questPositions) { q.gameObject.SetActive(false); }
    }
    void CountDownTimer()
    {
        Debug.Log("Start");
        ClosePhoneNotice();
        countDown = timer;
        DOVirtual.Float(countDown, 0, countDown, t => { countDown = t; })
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                LoadQuest(); EventController.instance.TakeACall();
            });
    }

    public void SetUp_Start_Destination_Position()
    {
        customerPositions[currentQuestId].gameObject.SetActive(true);
        //Routes
        mapRoutes.startingPoint = GameController.instance.player.transform;
        mapRoutes.destinationPoint = customerPositions[currentQuestId];
        mapRoutes.enabled = true;
        ClosePhoneNotice();
        tips = startTips;
    }
    public void SetDestinationPoint()
    {
        destinationPositions[currentQuestId].gameObject.SetActive(true);

        isPickedUpCustomer = true;
        mapRoutes.destinationPoint = destinationPositions[currentQuestId];
        mapRoutes.enabled = true;
    }
    public void Complete()
    {
        isPickedUpCustomer = false;
        mapRoutes.enabled = false;
        

        //reward
        totalReward = cash +tips;
    }

    void LoadQuest()
    {
        line = routes.transform.GetChild(0).GetComponent<LineRenderer>();

        currentQuestId = PlayerPrefs.GetInt("questID");
        if (currentQuestId < listQuests.Count)
        {
            currentQuest = listQuests[currentQuestId];
        }
        else
        {
            PlayerPrefs.SetInt("questID", 0);
            currentQuestId = PlayerPrefs.GetInt("questID");
        }
        mapRoutes.startingPoint = customerPositions[currentQuestId];
        mapRoutes.destinationPoint = destinationPositions[currentQuestId];
        mapRoutes.enabled = true;

        Invoke("CalculateDistanceAndCash", 0.5f);

    }

    public void CalculateDistanceAndCash()
    {
        float length = 0f;

        // Loop through each point in the line
        for (int i = 0; i < line.positionCount - 1; i++)
        {
            // Calculate the distance between consecutive points and add to the total length
            length += Vector3.Distance(line.GetPosition(i), line.GetPosition(i + 1));
        }
        //
        mapRoutes.enabled = false;
        distance = (Mathf.Round(length / 100 * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1));
        cash = distance * 50;
        distanceUI.text = "Distance: " + distance + " km";
        cashUI.text = "Receive: " + cash + " $";
        OpenPhoneNotice();
    }
    public void OpenPhoneNotice()
    {
        phonePanel.LoadData();
        phonePanel.gameObject.SetActive(true);
        phonePanel.transform.DOMoveY(0, 0.25f)
        .SetEase(Ease.Linear);
    }
    public void AcceptedCall()
    {
        phonePanel.transform.DOMoveY(-1000, 0.5f)
        .OnComplete(() => { phonePanel.gameObject.SetActive(false); })
        .SetEase(Ease.Linear);

    }
    public void ClosePhoneNotice()
    {
        phonePanel.transform.DOMoveY(-1000, 0.5f)
        .OnComplete(() => { phonePanel.gameObject.SetActive(false); })
        .SetEase(Ease.Linear);
    }
}
