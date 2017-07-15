//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class SoundManager : MonoBehaviour
//{
//    public AudioSource musicSource;
//    public List<AudioSource> sfxSource;

//    //Range of +- 5% pitch to have a slight difference in sound everytime.
//    public float lowPitchRange = 0.95f;
//    public float highPitchRange = 1.05f;

//    public bool muteSFX = false;
//    public bool muteBGM = false;
//    public float currBGMVol = 1.0f;
//    public float currSFXVol = 1.0f;

//    public static SoundManager instance = null;

//    AudioSource defaultSfxSource;
    
//    void Awake()
//    {
//        if (instance == null)
//            instance = this;
//        else if (instance != this)
//            Destroy(gameObject);
//        Debug.Log("Finished SoundInit Script");
//        DontDestroyOnLoad(gameObject);
//        defaultSfxSource = sfxSource[0];
//    }

//    /// <summary>
//    /// Plays a Single Clip, Usually Used for The Main Looping Music Source
//    /// </summary>
//    /// <param name="clip"></param>
//    public void PlaySingle(AudioClip clip, int sourceNum)
//    {
//        if (muteSFX)
//        {
//            Debug.Log("SFX is Muted");
//            return;
//        }

//        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

//        sfxSource[sourceNum].pitch = randomPitch;
//        sfxSource[sourceNum].clip = clip;
//        sfxSource[sourceNum].Play();
//    }

//    /// <summary>
//    /// used to generate random sound effects with a variation in pitch
//    /// </summary>
//    /// params is used by adding a passing in a comma separated list e.g. movestep1,movestep2,...
//    /// <param name="clips"></param>
//    public void PlayRandomSfx(int sourceNum, params AudioClip[] clips)
//    {
//        if (muteSFX)
//        {
//            Debug.Log("SFX is Muted");
//            return;
//        }

//        int randomIndex = Random.Range(0, clips.Length);
//        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

//        sfxSource[sourceNum].pitch = randomPitch;
//        sfxSource[sourceNum].clip = clips[randomIndex];
//        sfxSource[sourceNum].Play();
//    }

//}
