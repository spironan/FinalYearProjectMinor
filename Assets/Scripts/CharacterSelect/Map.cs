using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour 
{
    public Image icon; // the image of the map for map select
    public string mapName;//name of the scene to load
    public Dictionary<TEAM, Vector2> spawnLocations = new Dictionary<TEAM, Vector2>(); // where the player should spawn
}
