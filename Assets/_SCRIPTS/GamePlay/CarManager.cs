using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private RCC_CarControllerV3 controller;
    public Transform doorPosition;
    public Transform passengerSheet;
    private Rigidbody rig;
    private Animator anim;
    [SerializeField] GameObject humanModel;

    private void Awake()
    {
        controller = GetComponent<RCC_CarControllerV3>();
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        //EventController.instance.takeACall += DisableControlCar;
        EventController.instance.accepted += EnableControlCar;
        EventController.instance.refuse += EnableControlCar;
        EventController.instance.carStop += DisableControlCar;
        EventController.instance.setupDone += EnableControlCar;
        EventController.instance.getOut += EnablePassenger;
        EventController.instance.getIn += Pickup;
        EventController.instance.closeDoor += CloseDoor;
        EventController.instance.completeTrip += EnableControlCar;

        EventController.instance.repair += Repair;
    }

    void Pickup()
    {
        anim.CrossFade("Open", 0);
    }
    void CloseDoor()
    {
        anim.CrossFade("Close", 0);
    }
    void EnablePassenger()
    {
        humanModel.gameObject.SetActive(true);
        humanModel.GetComponent<Animator>().CrossFade("Exiting Car", 0);
    }
    void DisableControlCar()
    {
        controller.canControl = false;
        rig.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void EnableControlCar()
    {
        controller.canControl = true;
        rig.constraints = RigidbodyConstraints.None;
    }

    public void Repair()
    {
        controller.Repair();
    }
}