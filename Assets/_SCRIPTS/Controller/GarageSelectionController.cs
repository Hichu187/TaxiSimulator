using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSelectionController : MonoBehaviour
{
    public List<RCC_CarControllerV3> _spawnedVehicles = new List<RCC_CarControllerV3>();
    public Transform spawnPosition;
    public int selectedIndex = 0;
    [SerializeField] GameObject item;
    public List<GameObject> btns;
    private void Awake()
    {
        for (int i = 0; i < _spawnedVehicles.Count; i++)
        {
            GameObject spawnItem = Instantiate(item, item.transform.parent);
            btns.Add(spawnItem);
        }
        Destroy(item);
        PlayerPrefs.SetInt("selectedVehicel", 0);
    }
    private void Start()
    {
        CreateVehicles();
        foreach (var btn in btns)
        {
            btn.GetComponent<Button>().onClick.AddListener(() => SpawnVehicleByID(btn));
        }
        //Event
        EventController.instance.startDrive += SelectVehicle;
    }

    private void CreateVehicles()
    {
        // for (int i = 0; i <= RCC_DemoVehicles.Instance.vehicles.Length; i++)
        // {
        //     // Spawning the vehicle with no controllable, no player, and engine off. We don't want to let player control the vehicle while in selection menu.
        //     RCC_CarControllerV3 spawnedVehicle = RCC.SpawnRCC(Vehicles.Instance.vehicles[i], spawnPosition.position, spawnPosition.rotation, false, false, false);

        //     // Disabling spawned vehicle. 
        //     spawnedVehicle.transform.parent = spawnPosition;
        //     spawnedVehicle.gameObject.SetActive(false);

        //     // Adding and storing it in _spawnedVehicles list.
        //     _spawnedVehicles.Add(spawnedVehicle);
        // }
        SpawnVehicle();
    }
    // public void NextVehicle()
    // {
    //     selectedIndex++;
    //     // If index exceeds maximum, return to 0.
    //     if (selectedIndex > _spawnedVehicles.Count - 1)
    //         selectedIndex = 0;
    //     SpawnVehicle();
    // }

    // public void PreviousVehicle()
    // {
    //     selectedIndex--;
    //     // If index is below 0, return to maximum.
    //     if (selectedIndex < 0)
    //         selectedIndex = _spawnedVehicles.Count - 1;
    //     SpawnVehicle();
    // }

    public void SpawnVehicleByID(GameObject btn)
    {
        int id = btns.IndexOf(btn);

        for (int i = 0; i < _spawnedVehicles.Count; i++)
            _spawnedVehicles[i].gameObject.SetActive(false);

        // And enabling only selected vehicle.
        _spawnedVehicles[id].gameObject.SetActive(true);
        selectedIndex = id;
    }

    public void SpawnVehicle()
    {
        // Disabling all vehicles.
        for (int i = 0; i < _spawnedVehicles.Count; i++)
            _spawnedVehicles[i].gameObject.SetActive(false);

        // And enabling only selected vehicle.
        _spawnedVehicles[selectedIndex].gameObject.SetActive(true);

    }

    public void SelectVehicle()
    {
        // Save the selected vehicle for instantianting it on next scene.
        PlayerPrefs.SetInt("selectedVehicel", selectedIndex);

        //Scene Control
    }

    public void DeSelectVehicle()
    {
        // De-registers the vehicle.
        RCC.DeRegisterPlayerVehicle();

        // Resets position and rotation.
        _spawnedVehicles[selectedIndex].transform.position = spawnPosition.position;
        _spawnedVehicles[selectedIndex].transform.rotation = spawnPosition.rotation;

        // Kills engine and disables controllable.
        _spawnedVehicles[selectedIndex].KillEngine();
        _spawnedVehicles[selectedIndex].SetCanControl(false);

        // Resets the velocity of the vehicle.
        _spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().ResetInertiaTensor();
        _spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
        _spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
}
