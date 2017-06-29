using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
    Dictionary<string, GenericAudioSource> audioSources = new Dictionary<string, GenericAudioSource>();
    bool muted = false;
    float curVol = 1.0f;
    GenericAudioSource defaultAudioSource = null;

    //Range given to  pitch to have a slight difference in sound everytime.
    bool hasRandomPitch = false;
    float lowPitchRange = 1.0f;
    float highPitchRange = 1.0f;
    float currentPitch = 1.0f;

    public bool IsMuted() 
    {
        return muted;
    }

    public void ToggleMute()
    {
        muted = !muted;
        if (muted)
        {
            SetAllVolume(0.0f);
        }
        else
        {
            SetAllVolume(curVol);
        }
    }

    public bool HasRandomPitch()
    {
        return hasRandomPitch;
    }

    public void SetRandomPitch(float lowPitchRange, float highPitchRange = 1.0f)
    {
        hasRandomPitch = true;
        this.lowPitchRange = lowPitchRange;
        this.highPitchRange = highPitchRange;
    }

    public void UnRandomPitch()
    {
        hasRandomPitch = false;
    }

    public void SetPitch(float newPitch)
    {
        if(newPitch >= 0.0f && newPitch <= 1.0f)
            currentPitch = newPitch;
    }

    public int GetAudioCount()
    {
        return audioSources.Count;
    }

    public void SetAllVolume(float volume)
    {
        if (volume < 0.0f)
            Debug.Log("Volume Can't Be Set To Below Zero");
        else if (volume > 1.0f)
            Debug.Log("Volume Can't Be Set to Above One");

        foreach (KeyValuePair<string, GenericAudioSource> entry in audioSources)
        {
            entry.Value.SetVolume(volume);
        }
        curVol = volume;
        Debug.Log("Volume(s) Set To : " + volume);
    }

    public void AddAudioSource(string audioSourceName , GenericAudioSource audioSource)
    {
        audioSources.Add(audioSourceName, audioSource);
        if (defaultAudioSource == null)
            defaultAudioSource = audioSource;
    }

    public GenericAudioSource GetAudioSource(string audioSourceName)
    {
        if (CheckIfExist(audioSourceName)) 
            foreach (string key in audioSources.Keys)
                if (key == audioSourceName)
                    return audioSources[key];

        if (audioSourceName == "" && defaultAudioSource != null)
        {
            Debug.Log("Returning Default Audio Source!");
            return defaultAudioSource;
        }

        Debug.Log("Couldnt find audiosource with name : " + audioSourceName);
        return null;
    }

    public GenericAudioSource GetAudioSourceByIndex(int index)
    {
        if (CheckIfExist(index))
        {
            int counter = 0;
            foreach (string key in audioSources.Keys)
            {
                if (counter == index)
                    return audioSources[key];

                ++counter;
            }
        }

        Debug.Log("No Such AudioSource of Index : " + index + ". It Is either below 0 or Bigger Then Map size of : " + audioSources.Count);
        return null;
    }

    public bool CheckIfExist(string audioSourceName)
    {
        return audioSources.ContainsKey(audioSourceName);
    }

    public bool CheckIfExist(int index)
    {
        return (index >= 0 && index <= audioSources.Count);
    }

    public void PlayRandomClip(bool toLoop = false, bool replaceNext = false, string audioSourceName = "", params AudioClip[] clips)
    {
        if (muted)
        {
            Debug.Log("Audio is Muted");
            return;
        }

        int randomIndex = Random.Range(0, clips.Length);
        if (hasRandomPitch)
            currentPitch = Random.Range(lowPitchRange, highPitchRange);

        Play(clips[randomIndex], audioSourceName, toLoop, currentPitch, replaceNext);
    }

    public void PlayClip(AudioClip clip, bool toLoop = false, string audioSourceName = "", bool playNext = false, bool replaceNext = false)
    {
        if (muted)
        {
            Debug.Log("Audio is Muted");
            return;
        }

        if (hasRandomPitch)
            currentPitch = Random.Range(lowPitchRange, highPitchRange);

        Play(clip, audioSourceName, toLoop, currentPitch, playNext, replaceNext);
    }

    void Play(AudioClip clip, string audioSourceName = "", bool toLoop = false, float pitch = 1.0f, bool playNext = false, bool replaceNext = false)
    {
        GenericAudioSource audioSource = GetAudioSource(audioSourceName);
        if (audioSource != null)
        {
            if (audioSource.HasBacklog() && audioSource.IsMaxed())
            {
                Debug.Log(audioSourceName + " Is Full, Trying to search for another audioSource to Take Over the Job");
                bool found = false;
                foreach (KeyValuePair<string, GenericAudioSource> entry in audioSources)
                {
                    if (!entry.Value.HasBacklog())
                    { 
                        if (entry.Value.IsPlaying())
                            continue;
                    }
                    else if (entry.Value.IsMaxed())
                            continue;

                    found = true;
                    audioSource = entry.Value;

                    break;
                }
                if (!found)
                {
                    Debug.Log(" Can't Find Suitable Role to Take Over the Job, Using Default to take over");
                    audioSource = defaultAudioSource;
                }
                if (playNext)
                    if (replaceNext)
                        defaultAudioSource.ReplaceNext(clip, toLoop, pitch);
                    else
                        defaultAudioSource.PlayNext(clip, toLoop, pitch);
                else
                    defaultAudioSource.PlayOnSchedule(clip, toLoop, pitch);
            }
            else if (playNext)
                if (replaceNext)
                    audioSource.ReplaceNext(clip, toLoop, pitch);
                else
                    audioSource.PlayNext(clip, toLoop, pitch);
            else
                audioSource.PlayOnSchedule(clip, toLoop, pitch);
        }
        else
        {
            Debug.Log("Cant Find AudioSource of name : " + audioSourceName + " Are you sure you entered the right name ?");
        }
    }

    public void ChangeClip(AudioClip clip, bool toLoop = false, float pitch = 1.0f, bool replaceNext = false, string audioSourceName = "")
    {
        GenericAudioSource audioSource = GetAudioSource(audioSourceName);
        if (audioSource != null)
        {
            audioSource.ChangeClip(clip, toLoop, pitch, replaceNext);
        }
        else
        {
            Debug.Log("Cant Find AudioSourceName : " + audioSourceName + " Are you sure youve entered the right name?");
        }
    }
}
