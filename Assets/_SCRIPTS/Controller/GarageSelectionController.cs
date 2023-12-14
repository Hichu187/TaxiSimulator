using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSelectionController : MonoBehaviour
{
    public List<RCC_CarControllerV3> _spawnedVehicles = new List<RCC_CarControllerV3>();
    public Vehicle_Buy_Infomation vehicleInfor;
    public Transform spawnPosition;
    public int selectedIndex = 0;

    public List<VehicleData> vehicleDatas;
    [SerializeField] GameObject item;
    public List<GameObject> btns;
    private int curIndex;

    [Header("CANVAS")]
    public GameObject buyBtn;
    public GameObject driveCustomBtn;
    public Sprite backgroundItem;
    public Sprite border;
    public Sprite normalBg;
    public Sprite normalborder;
    public Sprite unpurchasedIcon;
    [Header("DATA")]
    public List<int> purchasedVehicle;
    public List<GameObject> unPurchased;
    private void Awake()
    {
        for (int i = 0; i < _spawnedVehicles.Count; i++)
        {
            GameObject spawnItem = Instantiate(item, item.transform.parent);
            btns.Add(spawnItem);
            spawnItem.transform.GetChild(0).GetComponent<Image>().sprite = unpurchasedIcon;

            for (int j = 0; j < vehicleDatas[i].star; j++)
            {
                spawnItem.transform.GetChild(1).transform.GetChild(j).gameObject.SetActive(true);
            }
        }
        Destroy(item);

        if (!PlayerPrefs.HasKey("selectedVehicel")) PlayerPrefs.SetInt("selectedVehicel", 0);
        if (!PlayerPrefs.HasKey("vehicelPurchased"))
        {
            PlayerPrefs.SetString("vehicelPurchased", "0/");
        }

        LoadPurchasedItem();
    }
    private void Start()
    {
        CreateVehicles();
        foreach (var btn in btns)
        {
            btn.GetComponent<Button>().onClick.AddListener(() => SpawnVehicleByID(btn));
        }

        SpawnVehicleByID(btns[PlayerPrefs.GetInt("selectedVehicel")]);


        //Event
        EventController.instance.startDrive += SelectVehicle;
    }

    private void CreateVehicles()
    {
        SpawnVehicle();
    }

    public void SpawnVehicleByID(GameObject btn)
    {
        int id = btns.IndexOf(btn);

        for (int i = 0; i < _spawnedVehicles.Count; i++)
        {
            if (btns[i].GetComponent<VehicleItem>().isPurchased == true)
            {
                btns[i].GetComponent<Image>().sprite = normalBg;
                btns[i].transform.GetChild(2).GetComponent<Image>().sprite = normalborder;
                btns[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

            }
            _spawnedVehicles[i].gameObject.SetActive(false);
        }

        // And enabling only selected vehicle.
        _spawnedVehicles[id].gameObject.SetActive(true);
        curIndex = id;
        if (btn.GetComponent<VehicleItem>().isPurchased == true)
        {
            selectedIndex = id;
            btns[id].GetComponent<Image>().sprite = backgroundItem;
            btns[id].transform.GetChild(2).GetComponent<Image>().sprite = border;
            btns[id].transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            vehicleInfor.CloseInfor();
        }
        else
        {
            vehicleInfor.OpenInfor(vehicleDatas[curIndex]);
        }
    }
    public void BuyVehicle()
    {
        if ( PlayerPrefs.GetFloat("cash") >= vehicleDatas[curIndex].price)
        {
            PlayerPrefs.SetString("vehicelPurchased", PlayerPrefs.GetString("vehicelPurchased") + curIndex + "/");
            btns[curIndex].GetComponent<VehicleItem>().isPurchased = true;
            btns[curIndex].transform.GetChild(0).GetComponent<Image>().sprite = vehicleDatas[curIndex].icon;
            SpawnVehicleByID(btns[curIndex]);
            Debug.Log(vehicleDatas[curIndex].price);
            DataManager.instance.AddCoinValue(-vehicleDatas[curIndex].price);
        }

    }
    public void SpawnVehicle()
    {
        // Disabling all vehicles.
        for (int i = 0; i < _spawnedVehicles.Count; i++)
            _spawnedVehicles[i].gameObject.SetActive(false);

        // And enabling only selected vehicle.
        _spawnedVehicles[selectedIndex].gameObject.SetActive(true);
        _spawnedVehicles[selectedIndex].transform.position = spawnPosition.position;

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


    public void LoadPurchasedItem()
    {
        string[] stringNumbers = PlayerPrefs.GetString("vehicelPurchased").Split('/');

        foreach (string str in stringNumbers)
        {
            if (int.TryParse(str, out int number))
            {
                purchasedVehicle.Add(number);
                btns[number].GetComponent<VehicleItem>().isPurchased = true;
                btns[number].transform.GetChild(0).GetComponent<Image>().sprite = vehicleDatas[number].icon;
            }
        }
    }
}
