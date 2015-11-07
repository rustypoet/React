using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPitch : MonoBehaviour
{
    public int startingPitch = 10;
    public int timeToDecrease = 5;
    AudioSource audioSource;

    void Start()
    {
		audioSource = GetComponent<AudioSource>();
		audioSource.pitch = startingPitch;
    }

    void Update()
    {
		if (audioSource.pitch > 0)
			audioSource.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
    }
}