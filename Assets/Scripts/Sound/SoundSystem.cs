using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundSystem : MonoBehaviour {

    Dictionary<AUDIO_TYPE, AudioManager> audioList = new Dictionary<AUDIO_TYPE, AudioManager>();

    public static SoundSystem instance = null;

	// Use this for initialization
	void Awake () 
    {
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

        foreach (AudioSourceScript audioSource in GetComponentsInChildren<AudioSourceScript>())
        {
            AddAudioSource(audioSource);
        }

        Initialze(DatabaseSystem.GetInstance().GetDataBase("FYPJ2Database"), "VolumeData");
        Debug.Log("Finished SoundSystem Initialization");
	}

    void Initialze(Database database, string tableName)
    {
        database.dbConnection.Open();
        database.dbCmd = database.dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tableName;
        database.dbCmd.CommandText = sqlQuery;
        database.reader = database.dbCmd.ExecuteReader();
        while (database.reader.Read())
        {
            for (AUDIO_TYPE i = AUDIO_TYPE.BACKGROUND_MUSIC; i < AUDIO_TYPE.END; ++i)
            {
                GetAudioManagerByType(i).SetAllVolume(database.reader.GetFloat((int)i));
            }
        }
        database.SoftReset();
        Debug.Log("Finished Creating AudioClip From Database");
    }

    void AddAudioSource(AudioSourceScript audioSource)
    {
        if(CheckIfExist(audioSource.gameObject.name))
        { 
            Debug.Log("Already Have AudioSource with name : " + audioSource + " Please Set it to a different name in the editor");
            return;
        }
        foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
        {
            if (entry.Key == audioSource.type)
            {
                entry.Value.AddAudioSource(audioSource.gameObject.name,audioSource);
            }
        }
    }

    // Get AudioManager
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

    public AudioManager GetAudioManagerByName(string name)
    {
        if (CheckIfExist(name))
        {
            foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
            {
                if (entry.Value.gameObject.name == name)
                {
                    return entry.Value;
                }
            }
        }
        Debug.Log("No Such audioSource with name : " + name + " Please Check you have an audioSource with that name with matching case");
        return null;
    }

    public bool CheckIfExist(AUDIO_TYPE type)
    {
        return type < AUDIO_TYPE.END;
    }

    public bool CheckIfExist(int index)
    {
        return index >= 0 && index <= audioList.Count;
    }

    public bool CheckIfExist(string name)
    {
        foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
        {
            if (entry.Value.gameObject.name == name)
            {
                return true;
            }
        }
        return false;
    }

    //Play Clip In Variant

    public void PlayClip(AUDIO_TYPE audioType, AudioClip clip, bool toLoop = false, string audioSourceName = "", bool playNext = false, bool replaceNext = false)
    {
        GetAudioManagerByType(audioType).PlayClip(clip, toLoop, audioSourceName, playNext, replaceNext);
    }

    public void ChangeClip(AUDIO_TYPE audioType, AudioClip clip, bool toLoop = false, float pitch = 1.0f, bool replaceNext = false, string audioSourceName = "")
    {
        GetAudioManagerByType(audioType).ChangeClip(clip, toLoop, pitch, replaceNext, audioSourceName);
    }





    //Everything Volume
    public void ToggleMute(AUDIO_TYPE type)
    {
        GetAudioManagerByType(type).ToggleMute();
    }

    //On Value Change for Slider effects on SFX
    public void OnValueChanged(Slider slider, AUDIO_TYPE audioType)
    {
        Debug.Log(" Volume Changed!");
        GetAudioManagerByType(audioType).SetAllVolume(slider.value);
    }

    public void ChangeAllVolume(float volume)
    {
        foreach (KeyValuePair<AUDIO_TYPE, AudioManager> entry in audioList)
        {
            entry.Value.SetAllVolume(volume);
        }
    }

    public void ChangeVolume(float volume, AUDIO_TYPE audioType)
    {
        GetAudioManagerByType(audioType).SetAllVolume(volume);
    }

    public float GetVolumeByType(AUDIO_TYPE audioType)
    {
        return GetAudioManagerByType(audioType).GetVolume();
    }


}
