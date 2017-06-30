using UnityEngine;
using System.Collections;

public class ToggleActiveScript : MonoBehaviour 
{
    public bool defaultActive = true;
    SoundSystem soundSystem;
    AudioClip openDisplay, closeDisplay;

	// Use this for initialization
    void Awake()
    {
        soundSystem = GameObject.FindWithTag("SoundSystem").GetComponent<SoundSystem>();
        gameObject.SetActive(defaultActive);
	}

    public void ToggleActive(bool playSound = true)
    {
        defaultActive = !defaultActive;
        SetActive(playSound);
    }

    public void ToggleActiveOn(bool playSound = true)
    {
        defaultActive = true;
        SetActive(playSound);
    }

    public void ToggleActiveOff(bool playSound = true)
    {
        defaultActive = false;
        SetActive(playSound);
    }

    void SetActive(bool playSound)
    {
        gameObject.SetActive(defaultActive);
        if(playSound)
            PlaySound();
    }

    void PlaySound()
    {
        if (openDisplay == null)
            openDisplay = AudioClipManager.GetInstance().GetAudioClip("OpenDisplay");
        if (closeDisplay == null)
            closeDisplay = AudioClipManager.GetInstance().GetAudioClip("CloseDisplay");

        if (defaultActive)
            soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, openDisplay);
        else
            soundSystem.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, closeDisplay);
    }

}
