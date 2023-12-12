using UnityEngine.SceneManagement;
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
    [SerializeField] List<QuestData> taxiModeQuests;

    public List<Transform> customerPositions;
    public List<Transform> destinationPositions;
    //public List<Transform> questPositions;
    [Header("=========")]
    [SerializeField] List<QuestData> challengeQuests;
    private float startTips = 100;
    public RCC_CarControllerV3 player;
    [Header("=========")]
    [Header("DATA")]
    [SerializeField] float timer;
    [SerializeField] MinimapRoutes mapRoutes;
    float countDown;
    int currentQuestId;
    int currentChallengeId;
    public int questTime = 0;
    int hitTime = 0;

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
    public GameObject time;

    void Start()
    {
        questTime = PlayerPrefs.GetInt("questTime");
        time.SetActive(false);
        //event
        EventController.instance.accepted += SetUp_Start_Destination_Position;
        EventController.instance.pickUpCustomer += SetDestinationPoint;
        EventController.instance.completeTrip += Complete;
        EventController.instance.startGame += CountDownTimer;
        EventController.instance.damage += VehicleHit;
        EventController.instance.refuse += CountDownTimer;
        EventController.instance.completeTrip += CountDownTimer;

        //
        SetUp();
    }

    void SetUp()
    {
        foreach (var c in customerPositions) { c.gameObject.SetActive(false); }
        foreach (var d in destinationPositions) { d.gameObject.SetActive(false); }
        //foreach (var q in questPositions) { q.gameObject.SetActive(false); }
    }
    void CountDownTimer()
    {
        player = GameController.instance.player;
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

        time.SetActive(true);
        TextMeshProUGUI text = time.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        DOVirtual.Int(currentQuest.timer, 0, currentQuest.timer, t =>
        {
            int minutes = Mathf.FloorToInt(t / 60);
            int seconds = Mathf.FloorToInt(t % 60);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }).SetEase(Ease.Linear);
    }
    public void SetDestinationPoint()
    {
        destinationPositions[currentQuestId].gameObject.SetActive(true);

        isPickedUpCustomer = true;
        mapRoutes.destinationPoint = destinationPositions[currentQuestId];
        mapRoutes.enabled = true;
        hitTime = 0;
        quality = RideQuality.Happy;
    }
    public void Complete()
    {
        isPickedUpCustomer = false;
        mapRoutes.enabled = false;
        //reward
        QualityCheck();
        totalReward = cash + tips;
        if (questTime < 2) { questTime++; PlayerPrefs.SetInt("questTime", questTime); }
    }

    void LoadQuest()
    {
        line = routes.transform.GetChild(0).GetComponent<LineRenderer>();

        if (questTime < 2)
        {
            currentQuestId = PlayerPrefs.GetInt("questID");
            if (currentQuestId < taxiModeQuests.Count)
            {
                currentQuest = taxiModeQuests[currentQuestId];
            }
            else
            {
                PlayerPrefs.SetInt("questID", 0);
                currentQuestId = PlayerPrefs.GetInt("questID");
            }
            mapRoutes.startingPoint = customerPositions[currentQuestId];
            mapRoutes.destinationPoint = destinationPositions[currentQuestId];
            mapRoutes.enabled = true;
        }
        else
        {
            currentChallengeId = PlayerPrefs.GetInt("challengeId");

            if (currentChallengeId < challengeQuests.Count)
            {
                currentQuest = challengeQuests[currentChallengeId];
            }
            else
            {
                PlayerPrefs.SetInt("challengeId", 0);
                currentChallengeId = PlayerPrefs.GetInt("challengeId");
            }
        }

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
        if (questTime < 2)
        {
            distance = (Mathf.Round(length / 100 * Mathf.Pow(10, 1)) / Mathf.Pow(10, 1));
            cash = distance * 50;
            distanceUI.text = distance + " km";
            cashUI.text = cash + " $";
        }
        else
        {
            distanceUI.text = "";
            cashUI.text = "";
        }

        //OpenPhoneNotice();
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

    public void VehicleHit(float damage)
    {
        if (isPickedUpCustomer)
        {
            hitTime++;
            if (tips > 0) tips -= 10f;
            Debug.Log(hitTime);
            QualityCheck();
        }

    }

    void QualityCheck()
    {
        switch (hitTime)
        {
            case 0:
                quality = RideQuality.Happy;
                break;
            case int n when n <= 3 && n > 0:
                quality = RideQuality.Good;
                break;
            case int n when n <= 5 && n > 3:
                quality = RideQuality.Shocked;
                break;
            case int n when n > 5:
                quality = RideQuality.Angry;
                break;
        }
    }
    public void QuestStart()
    {
        switch (currentQuest.mode)
        {
            case GameMode.Taxi:
                EventController.instance.AcceptedCall();
                break;
            case GameMode.Parking:
                PlayerPrefs.SetInt("questTime", 0);
                SceneController.instance.LoadParkingScene();
                break;
        }
    }
}
