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
    private float requiredTimeInsideTrigger = 3f;
    public List<GameObject> charModel;
    public GameObject curModel;
    public bool endPos;
    private void Start()
    {
        //curModel = charModel[PlayerPrefs.GetInt("questID")];
        EventController.instance.setupDone += PickupDone;

        if(endPos) transform.GetChild(0).gameObject.SetActive(false);
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
                    EventController.instance.CarStop();
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    EventController.instance.CarStop();
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

    private void PickupDone()
    {
        EventController.instance.PickUpCustomer();
    }

    private void CompleteTrip()
    {

    }
}
