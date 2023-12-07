using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : MonoBehaviour
{

    public void OpenDoor()
    {
        EventController.instance.GetInCar();
    }

    public void CloseDoor()
    {
        EventController.instance.CloseDoor();
    }

    public void SetUpDone()
    {
        EventController.instance.SetupDone();
    }

    public void CompleteTrip()
    {
        EventController.instance.CompleteTheTrip();
    }

}
