using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapsHolderScript : MonoBehaviour 
{
    public List<GameObject> maps;
    GameManager gameManager;
    // Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        foreach (GameObject map in maps)
        {
            if (map.name == gameManager.GetCurrMap().GetMapName())
                map.SetActive(true);
 
        } 
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
