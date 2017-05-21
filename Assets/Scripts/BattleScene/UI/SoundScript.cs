using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundScript : MonoBehaviour 
{
    public List<AudioClip> audioClips;
    AudioSource audioSource;

    public void PlayClip(string clipName)
    {
        foreach (AudioClip sound in audioClips)
        {
            if (sound.name == clipName)
            {
                audioSource.clip = sound;
                audioSource.Play();
            }
        }
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
}
