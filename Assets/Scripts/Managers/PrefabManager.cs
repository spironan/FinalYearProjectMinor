using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Singleton to store all the prefabs
public class PrefabManager : Singleton<PrefabManager>
{
    Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();
    string fullFilePath;//for easily changing file paths and writing lesser in database

    public void LoadPrefabs() { }

    public void Clear() { prefabList.Clear(); }

    public int GetPrefabCount() { return prefabList.Count; }

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
                if (!HasPrefab(name))
                {
                    GeneratePrefab(database.reader.GetString(0), database.reader.GetString(1));
                }
            }
            database.SoftReset();
            Debug.Log("Finished Creating Prefabs From Database");
        }
    }

    public GameObject GetPrefab(string filename)
    {
        if (HasPrefab(filename))
        {
            foreach (string key in prefabList.Keys)
            {
                if (key == filename)
                {
                    GameObject copy = GameObject.Instantiate(prefabList[key]);
                    return copy;
                }
            }
        }

        Debug.Log("No Such Prefab of Name : " + filename + " Exist, Please Create It First");
        return null;
    }

    GameObject GeneratePrefab(string fileName, string filePath)
    {
        fullFilePath = "Prefabs/" + filePath;
        if (HasPrefab(filePath))
        {
            Debug.Log("FileName Already Have an existing Prefab, returning the existing Prefab");
            return GetPrefab(fileName);
        }
        GameObject prefab = Resources.Load<GameObject>(fullFilePath);
        if (prefab != null)
        {
            Debug.Log("SuccessFully Loaded Prefab File :" + fullFilePath + " at FilePath : " + fullFilePath);
            prefabList.Add(fileName, prefab);
            return prefab;
        }

        Debug.Log("No Such FilePath :" + fullFilePath + " Have you loaded the right file?");
        return null;
    }

    public bool HasPrefab(string filename)
    {
        return prefabList.ContainsKey(filename);
    }

}
