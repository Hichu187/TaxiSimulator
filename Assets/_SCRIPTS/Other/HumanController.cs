using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    void Start()
    {
        anim = GetComponent<Animator>();
        EventController.instance.getIn += GetInCar;
        //EventController.instance.getOut += GetOutCar;
        EventController.instance.completeTrip += Waving;

        //player = FindObjectOfType<RCC_CarControllerV3>().gameObject;
    }

    void Waiting()
    {
        anim.CrossFade("idle1", 0);
    }

    void GetInCar()
    {
        player = FindObjectOfType<RCC_CarControllerV3>().gameObject;
        this.transform.position = player.GetComponent<CarManager>().doorPosition.position;
        this.transform.rotation = player.GetComponent<CarManager>().doorPosition.rotation;
        anim.CrossFade("Entering Car", 0);
    }

    void Waving()
    {
        transform.parent = null;
        anim.CrossFade("Waving", 0);
    }

    public void EnableObject()
    {
        this.gameObject.gameObject.SetActive(true);
    }
    public void DisableObject()
    {
        player.GetComponent<Animator>().CrossFade("Close", 0);
        player.GetComponent<RCC_CarControllerV3>().canControl = true;
        EventController.instance.PickUpCustomer();
        this.transform.parent.gameObject.SetActive(false);
    }
}
