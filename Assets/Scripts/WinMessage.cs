using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMessage : MonoBehaviour {

    public float msgDelay = 1.0f;
    
    AudioSource audio;
   
	void Start () {
        audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    public void PlayMsg()
    {
        StartCoroutine(PlayMsgWithDelay(msgDelay));
    } 

    IEnumerator PlayMsgWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audio.Play();
    }
}
