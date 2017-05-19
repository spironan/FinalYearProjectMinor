using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
    private Dictionary<PLAYMAPS, Map> maps = new Dictionary<PLAYMAPS, Map>();

    public MapManager() { } 

    public void Clear()
    {
        maps.Clear();
    }

    public void InitMaps(string database, string tableName)
    {
        //Create All Unique Characters Data Here through local database server,Should Only Be Ran Once On Initalization
        Database dataBase = DatabaseSystem.GetInstance().GetDataBase(database);
        dataBase.dbConnection.Open();
        dataBase.dbCmd = dataBase.dbConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tableName;
        dataBase.dbCmd.CommandText = sqlQuery;
        dataBase.reader = dataBase.dbCmd.ExecuteReader();
        while (dataBase.reader.Read())
        {
            string name = dataBase.reader.GetString(1);
            if (!HasMap(name))
            {
                //characterList.Add(dataBase.reader.GetInt32(0),
                //    new CharacterBase(name, dataBase.reader.GetInt32(2),
                //                 dataBase.reader.GetInt32(3), dataBase.reader.GetInt32(4), dataBase.reader.GetString(5),
                //                 dataBase.reader.GetString(6), dataBase.reader.GetString(7)));
            }
        }
        dataBase.SoftReset();

    }

    public Map GetMap(int mapID)
    {
        if (HasMap(mapID))
        {
            foreach (PLAYMAPS key in maps.Keys)
            {
                if (key == (PLAYMAPS)mapID)
                {
                    Map avatar = new Map(maps[key]);
                    return avatar;
                }
            }
        }

        Debug.Log("No Such Map of ID : " + mapID + " Exist, Please Create It First");
        return null;
    }

    public Map GetMap(PLAYMAPS mapID)
    {
        if (HasMap(mapID))
        {
            foreach (PLAYMAPS key in maps.Keys)
            {
                if (key == mapID)
                {
                    Map avatar = new Map(maps[key]);
                    return avatar;
                }
            }
        }

        Debug.Log("No Such Map : " + mapID.ToString() + " Exist, Please Create It First");
        return null;
    }

    public bool HasMap(int mapID)
    {
        return maps.ContainsKey((PLAYMAPS)mapID);
    }

    public bool HasMap(PLAYMAPS mapID)
    {
        return maps.ContainsKey(mapID);
    }

    public bool HasMap(string mapName)
    {
        foreach (PLAYMAPS key in maps.Keys)
        {
            if (maps[key].GetMapName() == mapName)
                return true;
        }
        return false;
    }

}
