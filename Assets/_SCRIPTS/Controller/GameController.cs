using System.Collections;
using System.Collections.Generic;
using GleyTrafficSystem;
using MTAssets.EasyMinimapSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject minimapRenderer;
    public TrafficComponent trafficComponent;
    public List<RCC_CarControllerV3> _spawnedVehicles = new List<RCC_CarControllerV3>();
    public Transform spawnPosition;
    public RCC_CarControllerV3 player;


    void Awake()
    {
        instance = this;

    }
    void Start()
    {
        SpawnSelectedVehicles();

    }

    void SpawnSelectedVehicles()
    {
        int vehicleID = PlayerPrefs.GetInt("selectedVehicel");
        _spawnedVehicles[vehicleID].transform.position = spawnPosition.position;
        _spawnedVehicles[vehicleID].gameObject.SetActive(true);
        trafficComponent.player = _spawnedVehicles[vehicleID].transform;
        trafficComponent.gameObject.SetActive(true);
        Invoke("StartGame", 1);
        //Invoke("SetupMinimapRenderer",5);
    }
    public void StartGame()
    {
        player = FindObjectOfType<RCC_CarControllerV3>();
        EventController.instance.GameStart();
        

    }
    void SetupMinimapRenderer()
    {
        minimapRenderer.SetActive(true);

    }
}
