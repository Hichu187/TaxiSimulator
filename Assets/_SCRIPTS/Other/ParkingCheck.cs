using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventController.instance.ParkingDone();
            this.transform.parent.gameObject.SetActive(false);

            PlayerPrefs.SetInt("parkingModeID", PlayerPrefs.GetInt("parkingModeID") + 1);
        }
    }
}
