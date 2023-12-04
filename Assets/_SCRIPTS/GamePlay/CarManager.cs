using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private RCC_CarControllerV3 controller;
    public Transform doorPosition;
    private Rigidbody rig;
    [SerializeField] GameObject humanModel;
    private void Awake()
    {
        controller = GetComponent<RCC_CarControllerV3>();
        rig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        EventController.instance.takeACall += DisableControlCar;
        EventController.instance.accepted += EnableControlCar;
        EventController.instance.refuse += EnableControlCar;
        EventController.instance.getOut += EnablePassenger;
        EventController.instance.getIn += Pickup;
    }

    void Pickup()
    {
        humanModel.gameObject.SetActive(true);
        humanModel.GetComponent<Animator>().CrossFade("Entering Car", 0);
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

    void EnableControlCar()
    {
        controller.canControl = true;
        rig.constraints = RigidbodyConstraints.None;
    }
}