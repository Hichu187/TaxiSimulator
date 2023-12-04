using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Vehicles", menuName = "Vehicles")]
public class Vehicles : ScriptableObject {

    public RCC_CarControllerV3[] vehicles;

    #region singleton
    private static Vehicles instance;
    public static Vehicles Instance { get { if (instance == null) instance = Resources.Load("Vehicles Asset/Vehicles") as Vehicles; return instance; } }
    #endregion

}