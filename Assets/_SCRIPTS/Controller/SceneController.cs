using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject loadingScene;
    public Image slider;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        EventController.instance.startDrive += LoadGameScene;
    }

    void LoadGameScene()
    {
        StartCoroutine(LoadLevel());
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
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

    public void LoadParkingScene()
    {
        SceneManager.LoadScene(2);
    }
}
