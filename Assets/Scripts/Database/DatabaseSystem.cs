using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DatabaseSystem : Singleton<DatabaseSystem>
{
    //a dictionary of the different database files and its Database(s)
    private Dictionary<string, Database> dataBaseSystem = new Dictionary<string, Database>();

    public DatabaseSystem()
    {
        dataBaseSystem.Clear();
    }

    public void InitDataBase(string dataBaseName, string dataBasePath)
    {
#if UNITY_ANDROID
        string actualDBFile = Application.persistentDataPath + "/" + dataBasePath;
        //string actualDBFile = Application.persistentDataPath + "/StudioProject4DataBase.db";
        //Debug.Log(actualDBFile);
        string filePath = "URI=file:" + actualDBFile;
        //Application.persistentDataPath
        if (!File.Exists(actualDBFile))
        {
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            //string ExactFilePath = "jar:file://" + Application.dataPath + "!/assets/" + dataBasePath;
            //Debug.Log(ExactFilePath);
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/StudioProject4DataBase.db");  // this is the path to your StreamingAssets in android
            while (!loadDB.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(actualDBFile, loadDB.bytes);
			Debug.Log("DB successfully created");
        }
#else
        string filePath = "URI=file:" + Application.dataPath + "/StreamingAssets/" + dataBasePath;
#endif
        CreateDataBase(dataBaseName, filePath);
    }

    public void Clear()
    {
        foreach (string key in dataBaseSystem.Keys)
        {
            dataBaseSystem[key].DropConnection();
        }
        dataBaseSystem.Clear();
    }

    public bool HasDataBase(string fileName)
    {
        return dataBaseSystem.ContainsKey(fileName);
    }

    public Database GetDataBase(string fileName)
    {
        if (HasDataBase(fileName))
        {
            foreach (string key in dataBaseSystem.Keys)
            {
                if (key == fileName)
                {
                    return dataBaseSystem[key];// Returns a FileReaderBase
                }
            }
        }

        Debug.Log("dataBase File Does Not Exist, Please Create it first");
        return null;
    }

    public Database GetCopy(string fileName)
    {
        if (HasDataBase(fileName))
        {
            foreach (string key in dataBaseSystem.Keys)
            {
                if (key == fileName)
                {
                    Database temp = new Database(dataBaseSystem[key]);
                    return temp;// Returns a FileReaderBase
                }
            }
        }

        Debug.Log("dataBase File Does Not Exist, Please Create it first");
        return null;
    }

    private Database CreateDataBase(string fileName, string filePath)
    {
        if (HasDataBase(fileName))
        {
            // I can Do replacing but lets play it safe there is tons of names anyway
            Debug.Log("File Name : " + fileName + " Already in Use,Please Use Another FileName");
            return GetDataBase(fileName);
        }
        Database dataBase = new Database();

        if (!dataBase.SetConnection(filePath))
            return null;

        dataBaseSystem.Add(fileName, dataBase);
        Debug.Log("Added into DataBaseSystem");

        return dataBase;
    }

}
