using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NewMainMenuScript : MonoBehaviour {

    enum MAIN_OPTIONS
    {
        START_PVP,
        SETTINGS,
        EXIT,
    };
    
    MAIN_OPTIONS button = MAIN_OPTIONS.START_PVP;
    Button[] buttons = null;
    ListOfControllerActions controller = null;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
