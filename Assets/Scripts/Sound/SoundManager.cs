using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource musicSource;

    //Range of +- 10% pitch to have a slight difference in sound everytime.
    public float lowPitchRange = 0.9f;
    public float highPitchRange = 1.1f;

    public bool muteSFX = false;
    public bool muteBGM = false;
    public float currBGMVol = 1.0f;
    public float currSFXVol = 1.0f;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        Debug.Log("Finished SoundInit Script");
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Plays a Single Clip, Usually Used for The Main Looping Music Source
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySingle(AudioClip clip)
    {
        if (muteSFX)
        {
            Debug.Log("SFX is Muted");
            return;
        }

        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    /// <summary>
    /// used to generate random sound effects with a variation in pitch
    /// </summary>
    /// params is used by adding a passing in a comma separated list e.g. movestep1,movestep2,...
    /// <param name="clips"></param>
    public void PlayRandomSfx(params AudioClip[] clips)
    {
        if (muteSFX)
        {
            Debug.Log("SFX is Muted");
            return;
        }

        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();
    }

}
