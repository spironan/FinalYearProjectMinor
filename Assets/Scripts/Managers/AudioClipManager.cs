using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Singleton to store all the prefabs
public class AudioClipManager : Singleton<AudioClipManager>
{
    Dictionary<string, AudioClip> audioclipList = new Dictionary<string, AudioClip>();
    string fullFilePath;//for easily changing file paths and writing lesser in database

    public void Clear() { audioclipList.Clear(); }

    public int GetClipCount() { return audioclipList.Count; }

    public void Initialze(Database database, string tableName)
    {
        if (!RanBefore())
        {
            //Create All Unique Characters Data Here through local database server,Should Only Be Ran Once On Initalization
            //database.dbConnection.Open();
            //database.dbCmd = database.dbConnection.CreateCommand();
            //string sqlQuery = "SELECT * FROM " + tableName;
            //database.dbCmd.CommandText = sqlQuery;
            //database.reader = database.dbCmd.ExecuteReader();
            database.SelectTable(tableName);
            while (database.reader.Read())
            {
                string name = database.reader.GetString(0);
                if (!HasAudioClip(name))
                {
                    GenerateAudioClip(database.reader.GetString(0), database.reader.GetString(1));
                }
            }
            database.SoftReset();
            Debug.Log("Finished Creating AudioClip From Database");
        }
    }

    public AudioClip GetAudioClip(string filename)
    {
        if (HasAudioClip(filename))
        {
            foreach (string key in audioclipList.Keys)
            {
                if (key == filename)
                {
                    return audioclipList[key];
                }
            }
        }

        Debug.Log("No Such AudioClip of Name : " + filename + " Exist, Please Create It First");
        return null;
    }

    AudioClip GenerateAudioClip(string fileName, string filePath)
    {
        fullFilePath = "Sounds/" + filePath;
        if (HasAudioClip(filePath))
        {
            Debug.Log("FileName Already Have an existing AudioClip, returning the existing Sprite");
            return GetAudioClip(fileName);
        }
        AudioClip prefab = Resources.Load<AudioClip>(fullFilePath);
        if (prefab != null)
        {
            Debug.Log("SuccessFully Loaded AudioClip File :" + fullFilePath + " at FilePath : " + fullFilePath);
            prefab.LoadAudioData();//Not Sure if this step is required
            audioclipList.Add(fileName, prefab);
            return prefab;
        }

        Debug.Log("No Such FilePath :" + fullFilePath + " Have you loaded the right file?");
        return null;
    }

    public bool HasAudioClip(string filename)
    {
        return audioclipList.ContainsKey(filename);
    }

}
