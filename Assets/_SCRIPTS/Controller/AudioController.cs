using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    void Awake() { instance = this; }

    public Slider musicSlider;
    public Slider soundSlider;
    public AudioSource musicSpeaker;
    public GameObject settingPanel;
    public List<AudioSource> soundSpeaker;
    public RCC_CarControllerV3 model;

    public AudioSource soundfx;
    public AudioClip completefx;
    public AudioClip ping;
    public AudioClip mess;
    public List<AudioClip> passegerTask1;
    public List<AudioClip> passegerTask2;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicValue")) PlayerPrefs.SetFloat("musicValue", 1);
        if (!PlayerPrefs.HasKey("soundValue")) PlayerPrefs.SetFloat("soundValue", 1);

        musicSlider.value = PlayerPrefs.GetFloat("musicValue") / 2.5f;
        soundSlider.value = PlayerPrefs.GetFloat("soundValue");
        setupAudio();

        EventController.instance.startGame += setupAudio;
        EventController.instance.takeACall += PhoneNotice;
        EventController.instance.openPhone += PassengerTalk1;
    }

    void Update()
    {
        if (settingPanel)
        {
            musicSpeaker.volume = musicSlider.value / 2.5f;

            foreach (AudioSource spk in soundSpeaker)
            {
                spk.volume = soundSlider.value;
            }
            if (model)
            {
                model.maxEngineSoundVolume = soundSlider.value;
                model.idleEngineSoundVolume = soundSlider.value;
            }
        }
    }

    void setupAudio()
    {

        model = FindObjectOfType<RCC_CarControllerV3>();

        AudioSource[] allAudioSources = Resources.FindObjectsOfTypeAll<AudioSource>();

        soundSpeaker.AddRange(allAudioSources);
        soundSpeaker.Remove(musicSpeaker);
    }
    public void SaveValue()
    {
        PlayerPrefs.SetFloat("musicValue", musicSlider.value);
        PlayerPrefs.SetFloat("soundValue", soundSlider.value);
    }

    void PhoneNotice()
    {
        soundfx.PlayOneShot(ping);
    }

    void PassengerTalk1()
    {
        Invoke("talk",0.5f);
    }

    void talk()
    {
        soundfx.PlayOneShot(passegerTask1[PlayerPrefs.GetInt("questID")]);
    }
}
