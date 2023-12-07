using System.Collections;
using System.Collections.Generic;
using MTAssets.EasyMinimapSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChallengeModeController : MonoBehaviour
{
    public static ChallengeModeController instance;
    public List<RCC_CarControllerV3> _spawnedVehicles = new List<RCC_CarControllerV3>();
    public Transform spawnPosition;
    public RCC_CarControllerV3 player;
    public List<GameObject> objectLevels;
    public GameObject currentLevelObject;

    [Header("Canvas")]
    public GameObject noticeCanvas;
    public GameObject loadingScene;
    public Image slider;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        noticeCanvas.SetActive(false);
        SpawnSelectedVehicles();
        SetUpLevel();

        EventController.instance.parkingDone += ParkingDone;
    }

    void SpawnSelectedVehicles()
    {
        int vehicleID = PlayerPrefs.GetInt("selectedVehicel");
        _spawnedVehicles[vehicleID].transform.position = spawnPosition.position;
        _spawnedVehicles[vehicleID].transform.rotation = spawnPosition.rotation;
        _spawnedVehicles[vehicleID].gameObject.SetActive(true);
        Invoke("StartGame", 1);
    }

    void SetUpLevel()
    {
        int parkingLv = PlayerPrefs.GetInt("parkingModelId");
        currentLevelObject = objectLevels[parkingLv];
        currentLevelObject.SetActive(true);

    }
    public void StartGame()
    {
        player = FindObjectOfType<RCC_CarControllerV3>();
    }

    public void ParkingDone()
    {
        player.canControl = false;
        Invoke("WinChallenge", 1.5f);
    }

    public void WinChallenge()
    {
        noticeCanvas.SetActive(true);

        if (PlayerPrefs.GetInt("parkingModelId") < objectLevels.Count)
        {
            PlayerPrefs.SetInt("parkingModelId", PlayerPrefs.GetInt("parkingModelId") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("parkingModelId", 0);
        }
    }
    public void ReturnMainScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        loadingScene.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(1);

        while (!op.isDone)
        {
            float prg = Mathf.Clamp01(op.progress / .9f);
            Debug.Log(prg);
            slider.fillAmount = prg;
            yield return null;
        }

    }
}
