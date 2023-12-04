using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DestinationCheck : MonoBehaviour
{
    private RCC_CarControllerV3 vehicle;
    private float timeInsideTrigger = 0f;
    private float requiredTimeInsideTrigger = 3f;
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
                StopCar();
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
    public void StopCar()
    {
        vehicle.canControl = false;
        vehicle.GetComponent<Animator>().CrossFade("Open", 0);

        //Event
        if (QuestController.instance.isPickedUpCustomer) EventController.instance.GetOutCar();
        else {EventController.instance.GetInCar();}
        
        //Invoke("ResetCar", 2);
    }

    private void ResetCar()
    {
        vehicle.GetComponent<Animator>().CrossFade("Close", 0);
        vehicle.canControl = true;
        if (!QuestController.instance.isPickedUpCustomer) EventController.instance.PickUpCustomer();
        else EventController.instance.CompleteTheTrip();
    }
}
