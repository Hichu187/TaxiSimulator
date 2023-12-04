using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MTAssets.EasyMinimapSystem;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController instance;
    void Awake() { instance = this; }
    [SerializeField] float timer;
    [SerializeField] MinimapRoutes mapRoutes;
    float countDown;
    int currentQuestId;
    public QuestData currentQuest;
    public Transform curDestination;
    [SerializeField] List<QuestData> listQuests;
    public List<Transform> customerPositions;
    public List<Transform> destinationPositions;
    public List<Transform> questPositions;
    public bool isPickedUpCustomer = false;
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
    }

    void LoadQuest()
    {
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

    }
}
