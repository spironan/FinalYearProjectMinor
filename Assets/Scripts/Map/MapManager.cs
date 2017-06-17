using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
    private Dictionary<string, Map> maps = new Dictionary<string, Map>();

    public MapManager() { } 

    public void Clear() { maps.Clear(); }

    public void InitMaps(Database database, string tableName)
    {
        if (!RanBefore())
        {
            //Create All Unique Characters Data Here through local database server,Should Only Be Ran Once On Initalization
            database.dbConnection.Open();
            database.dbCmd = database.dbConnection.CreateCommand();
            string sqlQuery = "SELECT * FROM " + tableName;
            database.dbCmd.CommandText = sqlQuery;
            database.reader = database.dbCmd.ExecuteReader();
            while (database.reader.Read())
            {
                string name = database.reader.GetString(0);
                TEAM team = TEAM.RED_TEAM;
                Vector2 newPos;
                float x, y;
                int readFrom;
                if (!HasMap(name))
                {
                    Map newMap = new Map();
                    newMap.SetMapName(database.reader.GetString(0));
                    newMap.SetMapSprite(SpriteManager.GetInstance().GetSprite(database.reader.GetString(0)));

                    readFrom = 2;
                    for (int i = 0; i < database.reader.GetInt32(1); ++i)
                    {
                        x = database.reader.GetFloat(readFrom);
                        y = database.reader.GetFloat(readFrom + 1);
                        readFrom += 2;

                        newPos = new Vector2(x, y);
                        newMap.AddToSpawnLocations(team, newPos);
                        team++;
                    }
                    maps.Add(newMap.GetMapName(), newMap);
                }
            }
            database.SoftReset();
            Debug.Log("Finished Creating Maps From Database");
        }
    }

    public int GetMapCount() { return maps.Count; }

    public Map GetMapByIndex(int index)
    {
        if (HasMap(index))
        {
            int counter = 0;
            foreach (string key in maps.Keys)
            {
                if (counter == index)
                {
                    Map map = new Map(maps[key]);
                    return map;
                }
                ++counter;
            }
        }

        Debug.Log("No Such Map of Index : " + index + ". It Is either below 0 or Bigger Then Map size of : " + maps.Count);
        return null;
    }
    public Map GetMap(string mapName)
    {
        if (HasMap(mapName))
        {
            foreach (string key in maps.Keys)
            {
                if (key == mapName)
                {
                    Map map = new Map(maps[key]);
                    return map;
                }
            }
        }

        Debug.Log("No Such Map of Name : " + mapName + " Exist, Please Create It First");
        return null;
    }

    public bool HasMap(string mapName)
    {
        return maps.ContainsKey(mapName);
    }
    public bool HasMap(int index)
    {
        return (index >= 0 && index <= maps.Count) ;
    }

}
