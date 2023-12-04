using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildModelController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Close()
    {
        transform.parent.GetComponent<Animator>().CrossFade("Close", 0);
        transform.parent.GetComponent<RCC_CarControllerV3>().canControl = true;
        EventController.instance.CompleteTheTrip();

    }
    public void PickUpPassenger()
    {
        transform.parent.GetComponent<Animator>().CrossFade("Close", 0);
        transform.parent.GetComponent<RCC_CarControllerV3>().canControl = true;
        EventController.instance.PickUpCustomer();
        this.gameObject.SetActive(false);
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
