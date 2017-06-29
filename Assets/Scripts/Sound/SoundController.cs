using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundController : MonoBehaviour
{
    GameObject SoundManager;
    SoundManager sound;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SoundManager = GameObject.FindWithTag("SoundManager");
        if (SoundManager == null)
            Debug.Log("SoundManager Could Not Be found, have you initialized it?");
        sound = SoundManager.GetComponent<SoundManager>();
    }

    //Mute or Unmute All SFX
    public void ToggleOnOffSFX()
    {
        if (sound.muteSFX)
        {
            Debug.Log("SFX Unmuted");
            for (int i = 0; i < sound.sfxSource.Count; ++i)
            { 
                sound.sfxSource[i].volume = sound.currSFXVol;
            }
            sound.muteSFX = false;
        }
        else
        {
            Debug.Log("SFX Muted");
            for (int i = 0; i < sound.sfxSource.Count; ++i)
            {
                sound.sfxSource[i].volume = 0.0f;
            }
            sound.muteSFX = true;
        }
    }
    
    //Mute or Unmute BGM
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

    //On Value Change for Slider effects on SFX
    public void OnValueChangedSfx(Slider slider)
    {
        if (!sound.muteSFX)
        {
            Debug.Log("SFX Volume Changed!");
            for (int i = 0; i < sound.sfxSource.Count; ++i)
            {
                sound.sfxSource[i].volume = slider.value;
            }
        }
        else
        {
            Debug.Log("SFX Volume Changed! Unmute to feel the difference!");
            sound.currSFXVol = slider.value;
        }
    }

    //On Value Change for Slider effects on BGM
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

    //Stop Current BGM from playing
    public void FinishCurrentBGM()
    {
        sound.musicSource.Stop();
    }

    //Stop All SFX from playing
    public void FinishCurrentSFX()
    {
        for (int i = 0; i < sound.sfxSource.Count; ++i)
        {
            sound.sfxSource[i].Stop();
        }
    }

    //Stop All Sounds From playing from all sources
    public void FinishCurrentAll()
    {
        FinishCurrentBGM();
        FinishCurrentSFX();
    }

    //Change BGM 
    public void ChangeBGM(AudioClip newBGM, bool toLoop = true)
    {
        FinishCurrentBGM();
        sound.musicSource.loop = toLoop;
        sound.musicSource.clip = newBGM;
        sound.musicSource.Play();
    }

    public void ChangeSFX(AudioClip newSFX, int sourceNum = 0)
    {
        FinishCurrentSFX();
        sound.sfxSource[sourceNum].clip = newSFX;
        sound.sfxSource[sourceNum].Play();
    }

    public void PlaySingle(AudioClip clip, int sourceNum = 0)
    {
        sound.PlaySingle(clip, sourceNum);
    }

    public void PlayRandomSFX(int sourceNum = 0, params AudioClip[] clips)
    {
        sound.PlayRandomSfx(sourceNum, clips);
    }
}
