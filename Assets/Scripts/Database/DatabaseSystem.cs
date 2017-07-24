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
        if (!File.Exists(newDatabaseInfo.databasePersistentPath))
        {
            WWW newDB = new WWW(newDatabaseInfo.newDatabasePath_Android);
            while (!newDB.isDone) { }
            File.WriteAllBytes(newDatabaseInfo.databasePersistentPath, newDB.bytes);
            //Debug.Log("persistent data path : " + newDatabaseInfo.databasePersistentPath);
            Debug.Log("new DB successfully created for android");
        }
#endif
        return CreateDataBase(newDatabaseInfo);
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

    //Dangerous to Get the actual db in case modified, used Get Copy for less risk
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

        Debug.Log("dataBase File of filename : " + fileName + " Does Not Exist, Please Create it first");
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
                    Database copy = new Database(dataBaseSystem[key]);
                    return copy;// Returns a FileReaderBase
                }
            }
        }

        Debug.Log("dataBase File of filename : " + fileName + " Does Not Exist, Please Create it first");
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
        Debug.Log("Added Database: " + newDatabaseInfo.databaseName + " into DataBaseSystem");

        return dataBase;
    }

}
