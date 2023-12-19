using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class DestinationCheck : MonoBehaviour
{
    private RCC_CarControllerV3 vehicle;
    private float timeInsideTrigger = 0f;
    private float requiredTimeInsideTrigger = 1.5f;
    public List<GameObject> charModel;
    public GameObject curModel;
    public bool endPos;
    private void Start()
    {
        //curModel = charModel[PlayerPrefs.GetInt("questID")];
        EventController.instance.setupDone += PickupDone;

        if (endPos) transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //  Getting car controller.
        RCC_CarControllerV3 carController = other.GetComponentInParent<RCC_CarControllerV3>();

        //  If trigger is not a vehicle, return.
        if (!carController)
            return;
        vehicle = carController;
        timeInsideTrigger = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (vehicle)
        {
            timeInsideTrigger += Time.deltaTime;
            if (timeInsideTrigger >= requiredTimeInsideTrigger)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;

                if (!QuestController.instance.isPickedUpCustomer)
                {
                    CarStop();
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    CarStop();
                }
            }
        }
        else return;
    }
    private void OnTriggerExit(Collider other)
    {
        if (vehicle)
        {
            if (timeInsideTrigger < requiredTimeInsideTrigger)
            {
                timeInsideTrigger = 0;
                vehicle = null;
            }
        }
        else return;
    }
    void CarStop()
    {
        EventController.instance.CarStop();
        this.transform.GetChild(0).GetComponent<ChildModelController>().CarStop();
    }
    private void PickupDone()
    {
        EventController.instance.PickUpCustomer();
    }

    private void CompleteTrip()
    {

    }
}
