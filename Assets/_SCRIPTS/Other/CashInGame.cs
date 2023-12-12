using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CashInGame : MonoBehaviour
{
    [SerializeField] int timer = 60;
    [SerializeField] int value = 500;
    private void OnTriggerEnter(Collider other)
    {
        //  Getting car controller.
        RCC_CarControllerV3 carController = other.GetComponentInParent<RCC_CarControllerV3>();

        //  If trigger is not a vehicle, return.
        if (!carController)
            return;

        EventController.instance.AddCash(value);

        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        StartTimer();
    }

    void StartTimer()
    {
        DOVirtual.Int(timer, 0, timer, t => { })
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<BoxCollider>().enabled = true;
        });
    }
}
