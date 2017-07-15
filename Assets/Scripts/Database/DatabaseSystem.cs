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

    public Database InitDataBase(string dataBaseName)
    {
        DatabaseInfo newDatabaseInfo = new DatabaseInfo(dataBaseName);
#if UNITY_ANDROID
        if (!File.Exists(newDatabaseInfo.databasePath))
        {
            WWW newDB = new WWW(newDatabaseInfo.newDatabasePath_Android);
            while (!newDB.isDone) { }
            File.WriteAllBytes(newDatabaseInfo.databasePath, newDB.bytes);
            Debug.Log("new DB successfully created for android");
        }
#endif
        return CreateDataBase(newDatabaseInfo);

//#if UNITY_ANDROID
//        string actualDBFile = Application.persistentDataPath + "/" + dataBasePath;
//        string filePath = "URI=file:" + actualDBFile;
//        if (!File.Exists(actualDBFile))
//        {
//            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/StudioProject4DataBase.db"); 
//            while (!loadDB.isDone) { }  
//            File.WriteAllBytes(actualDBFile, loadDB.bytes);
//            Debug.Log("DB successfully created");
//        }
//#else
//        string filePath = "URI=file:" + Application.dataPath + "/StreamingAssets/" + dataBasePath;
//#endif
//        return CreateDataBase(dataBaseName, filePath);
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

    private Database CreateDataBase(DatabaseInfo newDatabaseInfo)
    {
        if (HasDataBase(newDatabaseInfo.databaseName))
        {
            // I can Do replacing but lets play it safe there is tons of names anyway
            Debug.Log("Filename : " + newDatabaseInfo.databaseName + " Already in Use, Please Use Another Filename if you want to make a new one, Returning Existing Database");
            return GetDataBase(newDatabaseInfo.databaseName);
        }

        Database dataBase = new Database(newDatabaseInfo);

        if (!dataBase.SetConnection(newDatabaseInfo.fullfilePath))
            return null;

        dataBaseSystem.Add(newDatabaseInfo.databaseName, dataBase);
        Debug.Log("Added into DataBaseSystem");

        return dataBase;
    }

}
