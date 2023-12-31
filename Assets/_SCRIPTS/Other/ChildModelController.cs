using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class ChildModelController : MonoBehaviour
{
    public Animator anim;
    private NavMeshAgent agent;
    public CarManager vehicle;
    public bool isMoving;
    public List<GameObject> model;
    public GameObject curModel;
    void Start()
    {
        
    }
    void OnEnable()
    {
        //anim = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        vehicle = FindObjectOfType<CarManager>();

        //EventController.instance.carStop += CarStop;
        EventController.instance.setupDone += Disable;
        EventController.instance.setupDone += Disable;
        SetUpQuest();
    }
    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(this.transform.position, vehicle.doorPosition.position) <= 0.15f)
            {
                isMoving = false;
                agent.SetDestination(this.transform.position);
                this.transform.rotation = vehicle.doorPosition.rotation;
                anim.CrossFade("Entering Car", 0);
            }
        }

    }

    void SetUpQuest()
    {
        curModel = model[PlayerPrefs.GetInt("questID")];
        curModel.gameObject.SetActive(true);
        anim = curModel.GetComponent<Animator>();
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void CarStop()
    {
        if (!QuestController.instance.isPickedUpCustomer)
        {
            anim.CrossFade("Walking", 0);
            agent.SetDestination(vehicle.doorPosition.position);
            isMoving = true;
        }
        else
        {
            Debug.Log(vehicle.passengerSheet.position);
            agent.enabled = false;
            this.transform.position = vehicle.passengerSheet.position;
            Debug.Log(this.transform.position);
            this.transform.rotation = vehicle.passengerSheet.rotation;
            anim.CrossFade("Exiting Car", 0);
        }

    }
}
