using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Map 
{
    //Variable(s)
    Sprite mapSprite; // the image of the map for map select
    string mapName;//name of the scene to load
    Dictionary<TEAM, Vector2> spawnLocations = new Dictionary<TEAM, Vector2>(); // where the player should spawn
    
    //Constructor(s)
    public Map() { mapSprite = null; mapName = ""; spawnLocations.Clear(); }
    public Map(Map copy) 
    {
        mapSprite = copy.GetMapSprite();
        mapName = copy.GetMapName();
        spawnLocations = copy.GetSpawnLocations();
    }

    //Setter(s)
    public void SetMapSprite(Sprite mapSprite) { this.mapSprite = mapSprite; }
    public void SetMapName(string mapName) { this.mapName = mapName; }
    public void AddToSpawnLocations(TEAM team, Vector2 position) { spawnLocations.Add(team, position); }
    public void AddToSpawnLocations(int team, Vector2 position) 
    {
        if (team < 0)
            Debug.Log("Team No Must be Larger then Zero");

        if (team >= (int)TEAM.MAX_TEAM)
            Debug.Log("Team Number Trying to add too big :" + team + "Teams only exist up till : " + (int)TEAM.MAX_TEAM + "did you remember to -1?");

        spawnLocations.Add((TEAM)team, position);
    }

    //Getter(s)
    public Sprite GetMapSprite() { return mapSprite; }
    public string GetMapName() { return mapName; }
    public Vector2 GetSpawnLocation(TEAM team) 
    {
        foreach (TEAM teamNo in spawnLocations.Keys)
        {
            if(teamNo == team)
                return spawnLocations[teamNo];
        }

        Debug.Log("No such Team Exist , You Chose Team :" + team);
        return Vector2.zero;
    }
    public Vector2 GetSpawnLocation(int team)
    {
        foreach (TEAM teamNo in spawnLocations.Keys)
        {
            if ((int)teamNo == team)
                return spawnLocations[teamNo];
        }

        Debug.Log("No such Team Exist , You Chose Team :" + team);
        return Vector2.zero;
    }
    public Dictionary<TEAM, Vector2> GetSpawnLocations() { return spawnLocations; }
}
