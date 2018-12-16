using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GUISFX
{
    Load,
    Click
};

public class GUISoundController : MonoBehaviour {

    // Make sure clips are ordered in the same as the enum
    [Header("GUISFX audio clips")]
    public AudioClip[] sfxClips;                
    public static AudioSource[] sources;

    [Header("Volume Controls")]
    public static float masterVolume;
    public static float bgmVolume;
    public static float sfxVolume;

    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    /*
    static bool isAudioOn = false;
    AudioSource bgmAudio;
    */

    // To keep BGM persistent when changing levels
    void Awake()
    {
        // set up bgm audio 
        //bgmAudio = GetComponent<AudioSource>();

        // Set up sfx audio
        sources = new AudioSource[sfxClips.Length];
        for (int i = 0; i < sfxClips.Length; ++i)
        {
            GameObject child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            sources[i] = child.AddComponent<AudioSource>() as AudioSource;
            sources[i].clip = sfxClips[i];
        }

        // Play BGM audio if it's not currently being played
        /*
        if (!isAudioOn)
        {
            bgmAudio.Play();
            DontDestroyOnLoad(this.gameObject);
            isAudioOn = true;
        }
        // If BGM audio is being played then don't play any new BGM audio clips
        // otherwise, we'd get multiple BGMs playing at once
        else
        {
            bgmAudio.Stop();
        }
        */
    }

    void Start()
    {
        // Setup the volume sliders
        if (masterVolumeSlider == null)
        {
            masterVolumeSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
        }
        if (bgmVolumeSlider == null)
        {
            bgmVolumeSlider = GameObject.FindGameObjectWithTag("BGMSlider").GetComponent<Slider>();
        }
        if (sfxVolumeSlider == null)
        {
            sfxVolumeSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
        }

        /*if (masterVolumeSlider == null)
        {
            GameObject masterVolumeSliderObject = GameObject.Find("MasterSlider");
            if (masterVolumeSliderObject != null)
            {
                masterVolumeSlider = masterVolumeSliderObject.GetComponent<Slider>();
            }
        }
        if (bgmVolumeSlider == null)
        {
            GameObject bgmVolumeSliderObject = GameObject.Find("BGMSlider");
            if (bgmVolumeSliderObject != null)
            {
                bgmVolumeSlider = bgmVolumeSliderObject.GetComponent<Slider>();
            }
        }
        if (sfxVolumeSlider == null)
        {
            GameObject sfxVolumeSliderObject = GameObject.Find("SFXSlider");
            if (sfxVolumeSliderObject != null)
            {
                sfxVolumeSlider = sfxVolumeSliderObject.GetComponent<Slider>();
            }
        }*/

        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeValueChanged(); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate { OnBGMVolumeValueChanged(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSFXVolumeValueChanged(); });
        
    }

    void Update()
    {
        if (masterVolumeSlider != null)
        {
            masterVolume = masterVolumeSlider.value;
        }
        if (bgmVolumeSlider != null)
        {
            bgmVolume = bgmVolumeSlider.value;
            // Update bgm volume
            //bgmAudio.volume = Mathf.Min(bgmVolume, masterVolume);
        }       
        if (sfxVolumeSlider != null)
        {
            sfxVolume = sfxVolumeSlider.value;
        }
    }

    public static void Play(int soundIndex)
    {
        sources[soundIndex].volume = Mathf.Min(sfxVolume, masterVolume);  // Play at specified volume
        sources[soundIndex].Play();
    }

    // Use this version to manually modify the volume level
    // For example, if we want to change the volume priority of different events
    public static void Play(int soundIndex, float volumeLevel)  
    {
        volumeLevel = Mathf.Clamp(volumeLevel, 0.0f, 1.0f);
        //Debug.Log(soundIndex);
        sources[soundIndex].volume = volumeLevel * Mathf.Min(sfxVolume, masterVolume);
        sources[soundIndex].Play();
    }

    public static void PlayWithoutInterruption(int soundIndex)
    {
        AudioSource clip = sources[soundIndex];
        if (!clip.isPlaying)
        {
            clip.volume = Mathf.Min(sfxVolume, masterVolume);
            clip.Play();
        }
    }

    // Refactor to use these functions without using attached sliders maybe?
    public void OnMasterVolumeValueChanged()
    {
        masterVolume = masterVolumeSlider.value;
    }

    public void OnBGMVolumeValueChanged()
    {
        bgmVolume = bgmVolumeSlider.value;
    }

    public void OnSFXVolumeValueChanged()
    {
        sfxVolume = sfxVolumeSlider.value;
    }
}
