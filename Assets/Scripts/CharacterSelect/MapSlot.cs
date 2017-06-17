using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSlot : MonoBehaviour 
{
    public GameObject left = null, right = null;
    string mapName;

    public void SetMapName(string mapName) { this.mapName = mapName; }
    public string GetMapName() { return mapName; }
}
