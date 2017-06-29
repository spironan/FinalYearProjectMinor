using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundSystem : MonoBehaviour {

    Dictionary<AUDIO_TYPE, AudioManager> audioList = new Dictionary<AUDIO_TYPE, AudioManager>();

    public static SoundSystem instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        for (AUDIO_TYPE i = AUDIO_TYPE.BACKGROUND_MUSIC; i < AUDIO_TYPE.END; ++i)
        {
            AudioManager newAudioManager = gameObject.AddComponent<AudioManager>();
            audioList.Add(i, newAudioManager);
        }

        foreach (GenericAudioSource audioSource in GetComponentsInChildren<GenericAudioSource>())
        {
            AddAudioSource(audioSource);
        }

        Debug.Log("Finished SoundInit Script");
	}

    void AddAudioSource(GenericAudioSource audioSource)
    {
        foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
        {
            if (entry.Key == audioSource.type)
                entry.Value.AddAudioSource(audioSource.gameObject.name,audioSource);
        }
    }

    public AudioManager GetAudioManagerByType(AUDIO_TYPE type)
    {
        if(CheckIfExist(type))
            return audioList[type];

        Debug.Log("No Such Type Inputted : " + type + " Please Do Not use the last variable");
        return null;
    }

    public AudioManager GetAudioManagerByIndex(int index)
    {
        if (CheckIfExist(index))
        {
            int counter = 0;
            foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
            {
                if(counter == index)
                    return audioList[entry.Key];

                ++counter;
            }
        }
        Debug.Log("No Such index : " + index + " Please Check its more than zero and less than : " + (int)AUDIO_TYPE.END);
        return null;
    }

    public void PlayClip(AUDIO_TYPE audioType, AudioClip clip, bool toLoop = false)
    {
        GetAudioManagerByType(audioType).PlayClip(clip, toLoop);
    }

    public void ChangeClip(AUDIO_TYPE audioType, AudioClip clip, bool toLoop = false, float pitch = 1.0f, bool replaceNext = false, string audioSourceName = "")
    {
        GetAudioManagerByType(audioType).ChangeClip(clip, toLoop, pitch, replaceNext, audioSourceName);
    }

    //On Value Change for Slider effects on SFX
    public void OnValueChanged(Slider slider, AUDIO_TYPE audioType)
    {
        Debug.Log(" Volume Changed!");
        GetAudioManagerByType(audioType).SetAllVolume(slider.value);
    }

    public void ToggleMute(AUDIO_TYPE type)
    {
        GetAudioManagerByType(type).ToggleMute();
    }

    public bool CheckIfExist(AUDIO_TYPE type)
    {
        return type < AUDIO_TYPE.END;
    }

    public bool CheckIfExist(int index)
    {
        return index >= 0 && index <= audioList.Count;
    }

}
