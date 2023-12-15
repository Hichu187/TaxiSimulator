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
    public AudioClip opendoor;
    public AudioClip closedoor;

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
        EventController.instance.closeDoor += Closedoor;
        EventController.instance.getIn += Opendoor;
        EventController.instance.completeTrip += CompleteQuest;
    }

    void Update()
    {
        if (settingPanel)
        {
            musicSpeaker.volume = musicSlider.value / 5f;

            foreach (AudioSource spk in soundSpeaker)
            {
                if (spk != null)
                {
                    spk.volume = soundSlider.value;
                }

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
        if (QuestController.instance.questTime != 2)
        {
            Invoke("talk", 0.5f);
        }
    }

    public void PassengerTalk2()
    {
        soundfx.PlayOneShot(passegerTask2[PlayerPrefs.GetInt("questID")]);
    }


    void talk()
    {
        soundfx.PlayOneShot(passegerTask1[PlayerPrefs.GetInt("questID")]);
    }

    void Opendoor()
    {
        soundfx.PlayOneShot(opendoor);
    }
    void Closedoor()
    {
        soundfx.PlayOneShot(closedoor);
    }

    void CompleteQuest()
    {
        soundfx.PlayOneShot(completefx);
    }
}
