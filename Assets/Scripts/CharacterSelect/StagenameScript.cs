using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StagenameScript : MonoBehaviour 
{
    MapSelectScript mapselect;
    Text stageText;
	
    // Use this for initialization
	void Start () {
        mapselect = GameObject.FindWithTag("MapSpawnArea").GetComponent<MapSelectScript>();
        stageText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        if (stageText.text != mapselect.GetCurrentMapName())
            stageText.text = mapselect.GetCurrentMapName().ToUpper();
	}
}
