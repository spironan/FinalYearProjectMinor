using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public enum AUDIO_TYPE
{
    BACKGROUND_MUSIC,
    SOUND_EFFECTS,
    END,
};

public class GenericAudioSource : MonoBehaviour {

    public AUDIO_TYPE type = AUDIO_TYPE.BACKGROUND_MUSIC;

    public enum MODES
    {
        SINGLE,
        BACKLOG_MODE,
    };
    public MODES mode = MODES.SINGLE;

    public bool hasMaximumBacklog = false;
    public uint maxBacklog = 0;

    AudioSource audioSource;
    List<AudioClipData> audioBacklog = null;

    bool hasBacklog = false;
    bool isMaxed = false;

    bool stopPlaying = false;
    string name = "GenericAudioSource";

    protected void SetName(string name)
    {
        this.name = name;
    }

    protected string GetName()
    {
        return name;
    }

    public void SetVolume(float volume)
    {
        if(volume >= 0.0f && volume <= 1.0f)
            audioSource.volume = volume;
    }

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (mode == MODES.BACKLOG_MODE)
        {
            audioBacklog = new List<AudioClipData>();
            hasBacklog = true;
        }
    }

    public bool HasBacklog()
    {
        return hasBacklog;
    }

    public bool IsMaxed()
    {
        return isMaxed;
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    void CheckIfMaxed()
    {
        if (mode == MODES.BACKLOG_MODE && hasMaximumBacklog)
            if (audioBacklog.Count >= maxBacklog)
                isMaxed = true;
            else
                isMaxed = false;
    }

    public void PlayOnSchedule(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        if (hasBacklog)
        {
            AudioClipData clip = new AudioClipData(newClip, toLoop, pitch);
            if (hasMaximumBacklog)
            {
                if (!isMaxed)
                {
                    audioBacklog.Add(clip);
                    CheckIfMaxed();
                }
            }
            else
                audioBacklog.Add(clip);
        }
        else
            PlayClip(newClip, toLoop, pitch);
    }

    public void ReplaceNext(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        if (hasBacklog && audioBacklog.Count > 0)
        {
            AudioClipData clip = new AudioClipData(newClip, toLoop, pitch);
            audioBacklog[0] = clip;
        }
        else
            PlayOnSchedule(newClip, toLoop, pitch);
    }

    public void PlayNext(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        if (hasBacklog && audioBacklog.Count > 0)
        {
            AudioClipData clip = new AudioClipData(newClip, toLoop, pitch);
            if (hasMaximumBacklog && isMaxed)
            {
                Debug.Log("Maximum Limit of Sounds Set to : " + maxBacklog + " Cant Play Anymore Sounds Because backlog is full alr");
                return;
            }

            List<AudioClipData> newBackLog = new List<AudioClipData>();
            newBackLog.Add(clip);
            for (int i = 0; i < audioBacklog.Count; ++i)
            {
                newBackLog.Add(audioBacklog[i]);
            }
            audioBacklog.Clear();
            audioBacklog.AddRange(newBackLog);
        }
        else
            PlayOnSchedule(newClip, toLoop, pitch);
    }

    void PlayClip(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        audioSource.pitch = pitch;
        audioSource.loop = toLoop;
        audioSource.clip = newClip;
        audioSource.Play();
    }

    void PlayClip(AudioClipData newClip)
    {
        audioSource.pitch = newClip.pitch;
        audioSource.loop = newClip.toLoop;
        audioSource.clip = newClip.audioClip;
        audioSource.Play();
        audioBacklog.Remove(newClip);
    }

    public void StopImmediately()
    {
        audioSource.Stop();
        stopPlaying = false;
    }

    public void StopSource()
    {
        stopPlaying = true;
    }

    public void ClearAll()
    {
        if (hasBacklog)
            audioBacklog.Clear();
    }

    public void ImmediateClearAndStop()
    {
        if (hasBacklog)
            ClearAll();
        StopImmediately();
    }

    public void ClearAndPlay(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        if (hasBacklog)
            ClearAll();

        PlayClip(newClip, toLoop, pitch);
    }

    public void ImmediateClearAndPlay(AudioClip newClip, bool toLoop = false, float pitch = 1.0f)
    {
        ImmediateClearAndStop();
        PlayClip(newClip, toLoop, pitch);
    }

    public void ChangeClip(AudioClip newClip, bool toLoop = false, float pitch = 1.0f, bool toReplace = false)
    {
        if (hasBacklog)
            StopImmediately();
        if (toReplace)
            ReplaceNext(newClip, toLoop, pitch);
        else
            PlayNext(newClip, toLoop, pitch);
    }

    // Logic of the Different Modes
    void Update()
    {
        switch (mode)
        {
            case MODES.BACKLOG_MODE:
                {
                    if (!IsPlaying() && audioBacklog.Count > 0)
                    {
                        if (stopPlaying)
                            StopImmediately();
                        else
                            PlayClip(audioBacklog[0]);

                        if (hasMaximumBacklog && isMaxed)
                            CheckIfMaxed();
                    }
                }
                break;

            case MODES.SINGLE:
                {
                    if (!IsPlaying())
                    {
                        if (stopPlaying)
                            StopImmediately();
                    }
                }
                break;
        }

    }
}

[CustomEditor(typeof(GenericAudioSource))]
public class MyScriptEditor : Editor
{
    void OnInspectorGUI()
    {
        GenericAudioSource myScript = target as GenericAudioSource;

        //if (myScript.HasBacklog())
            //GUILayout.Toggle(myScript.hasMaximumBacklog, "Maximum backlog");

        //if (myScript.hasMaximumBacklog)
        //    GUILayout.Toggle(myScript.maxBacklog, "Flag");
        //myScript.i = EditorGUILayout.IntSlider("I field:", myScript.i, 1, 100);
    }
}