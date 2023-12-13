using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationControl : MonoBehaviour
{
    void Start()
    {
        EventController.instance.addCash += MiniVibration;
        EventController.instance.takeACall += LongVibration;
    }


    void SuperminiVibration(int value)
    {
        Vibrator.Vibrate(10);
    }
    void MiniVibration(int value)
    {
        Vibrator.Vibrate(50);
    }
    void ShortVibration()
    {
        Vibrator.Vibrate(100);
    }
    void MediumVibration()
    {
        Vibrator.Vibrate(250);
    }
    void LongVibration()
    {
        Vibrator.Vibrate(500);
    }
}
