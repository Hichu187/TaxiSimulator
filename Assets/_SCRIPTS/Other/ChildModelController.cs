using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class ChildModelController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    public CarManager vehicle;
    public bool isMoving;
    void OnEnable()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        vehicle = FindObjectOfType<CarManager>();

        EventController.instance.carStop += CarStop;
        EventController.instance.setupDone += Disable;
        EventController.instance.setupDone += Disable;

    }
    void Update()
    {
        if (isMoving)
        {
            Debug.Log(Vector3.Distance(this.transform.position, vehicle.doorPosition.position));
            if (Vector3.Distance(this.transform.position, vehicle.doorPosition.position) <= 0.1f)
            {
                isMoving = false;
                agent.SetDestination(this.transform.position);
                this.transform.rotation = vehicle.doorPosition.rotation;
                anim.CrossFade("Entering Car", 0);
            }
        }

    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    void CarStop()
    {
        if (!QuestController.instance.isPickedUpCustomer)
        {
            anim.CrossFade("Walking", 0);
            agent.SetDestination(vehicle.doorPosition.position);
            isMoving = true;
        }
        else
        {
            this.transform.position = vehicle.passengerSheet.position;
            this.transform.rotation = vehicle.passengerSheet.rotation;
            anim.CrossFade("Exiting Car", 0);
        }

    }
}
