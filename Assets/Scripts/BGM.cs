using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

    // TODO combine this with the SoundController
	static bool isAudioOn = false;
	new AudioSource audio;

    private GameObject guiSoundObj;
    private GameObject soundObj;

	// To keep BGM persistent when changing levels
	void Awake() {
		audio = GetComponent<AudioSource> ();
		if (!isAudioOn) { 
			audio.Play ();
			DontDestroyOnLoad (this.gameObject);
			isAudioOn = true;
		} else {
			audio.Stop ();
		}
	}

    void Update()
    {
        guiSoundObj = GameObject.FindGameObjectWithTag("GUISoundController");
        soundObj = GameObject.FindGameObjectWithTag("SoundController");
        if (soundObj != null)
        {
            //Debug.Log("Found sound obj");
            audio.volume = SoundController.bgmVolume * SoundController.masterVolume;
        }
        else if (guiSoundObj != null)
        {
            //Debug.Log("Found GUI sound obj");
            audio.volume = GUISoundController.bgmVolume * GUISoundController.masterVolume;
        }
    }

}
