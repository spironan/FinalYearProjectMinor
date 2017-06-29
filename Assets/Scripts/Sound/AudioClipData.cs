using UnityEngine;
using System.Collections;

public class AudioClipData
{
    public AudioClip audioClip = null;
    public float pitch = 1.0f;
    public bool toLoop = false;

    public AudioClipData(AudioClip clip, bool toLoop = false, float pitch = 1.0f)
    {
        this.audioClip = clip;
        this.toLoop = toLoop;
        this.pitch = pitch;
    }

    public AudioClipData(AudioClipData audioClipData)
    {
        audioClip = audioClipData.audioClip;
        toLoop = audioClipData.toLoop;
        pitch = audioClipData.pitch;
    }
}
