using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vehicle Data 0", menuName = "Vehicle")]
public class VehicleData : ScriptableObject
{
    public int id;
    public string vehicleName;
    public Sprite icon;
    public int star;
    public int price;
    public int topSpeed;
    public int acceleration;
    public int handling;
    public bool isAds;
    public int adsPrice;
}
