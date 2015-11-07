using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPitch : MonoBehaviour
{
    public int startingPitch = 10;
    public int timeToDecrease = 5;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.pitch = startingPitch;
    }

    void Update()
    {
        if (audio.pitch > 0)
            audio.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
    }
}