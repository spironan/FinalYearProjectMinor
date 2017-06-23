using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSlot : MonoBehaviour 
{
    public GameObject left = null, right = null;
    string mapName;
    Image mapImage;

    void Awake() { mapImage = GetComponent<Image>(); }
    public void SetMapSprite(Sprite mapSprite) { mapImage.sprite = mapSprite; }
    public void SetMapName(string mapName) { this.mapName = mapName; }
    public string GetMapName() { return mapName; }
}
