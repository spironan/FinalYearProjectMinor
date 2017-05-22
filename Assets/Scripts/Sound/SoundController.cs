using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundController : MonoBehaviour
{
    GameObject SoundManager;
    SoundManager sound;

    void Awake()
    {
        SoundManager = GameObject.FindWithTag("SoundManager");
        if (SoundManager == null)
            Debug.Log("SoundManager Could Not Be found, have you initialized it?");
        sound = SoundManager.GetComponent<SoundManager>();
    }

    public void ToggleOnOffSFX()
    {
        if (sound.muteSFX)
        {
            Debug.Log("SFX Unmuted");
            sound.sfxSource.volume = sound.currSFXVol;
            sound.muteSFX = false;
        }
        else
        {
            Debug.Log("SFX Muted");
            sound.sfxSource.volume = 0.0f;
            sound.muteSFX = true;
        }
    }

    public void ToggleOnOffBGM()
    {
        if (sound.muteBGM)
        {
            Debug.Log("SFX Unmuted");
            sound.musicSource.volume = sound.currBGMVol;
            sound.muteBGM = false;
        }
        else
        {
            Debug.Log("SFX Muted");
            sound.muteBGM = true;
            sound.musicSource.volume = 0.0f;
        }
    }

    public void OnValueChangedSfx(Slider slider)
    {
        if (!sound.muteSFX)
        {
            Debug.Log("SFX Volume Changed!");
            sound.sfxSource.volume = slider.value;
        }
        else
        {
            Debug.Log("SFX Volume Changed! Unmute to feel the difference!");
            sound.currSFXVol = slider.value;
        }
    }

    public void OnValueChangedBGM(Slider slider)
    {
        if (!sound.muteBGM)
        {
            Debug.Log("BGM Volume Changed!");
            sound.musicSource.volume = slider.value;
        }
        else
        {
            Debug.Log("BGM Volume Changed! Unmute to feel the difference!");
            sound.currBGMVol = slider.value;
        }
    }

    public void FinishCurrentBGM()
    {
        sound.musicSource.Stop();
    }

    public void FinishCurrentSFX()
    {
        sound.sfxSource.Stop();
    }

    public void FinishCurrentAll()
    {
        FinishCurrentBGM();
        FinishCurrentSFX();
    }

    public void ChangeBGM(AudioClip newBGM, bool toLoop = true)
    {
        FinishCurrentBGM();
        sound.musicSource.loop = toLoop;
        sound.musicSource.clip = newBGM;
        sound.musicSource.Play();
    }

    public void ChangeSFX(AudioClip newSFX)
    {
        FinishCurrentSFX();
        sound.sfxSource.clip = newSFX;
        sound.sfxSource.Play();
    }

    public void PlaySingle(AudioClip clip)
    {
        sound.PlaySingle(clip);
    }

    public void PlayRandomSFX(params AudioClip[] clips)
    {
        sound.PlayRandomSfx(clips);
    }
}
