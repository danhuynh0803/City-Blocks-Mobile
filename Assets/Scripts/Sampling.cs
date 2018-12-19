using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampling : MonoBehaviour
{
    public AudioClip music;
    public float[] samples;
    private AudioSource audioSource;
    private float timer;
    private float pause = 0.1f;
    private float clipLoudness;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samples = new float[1024];
        music.GetData(samples, 0);

    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= pause)
        {
            pause = 0f;
            audioSource.clip.GetData(samples, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (float sample in samples)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= 1024;
        }

    }

}
