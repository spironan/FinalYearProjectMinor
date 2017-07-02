using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapsHolderScript : MonoBehaviour 
{
    public List<GameObject> maps;

    // Use this for initialization
	void Start () {
        foreach (GameObject map in maps)
        {
            if (map.name == GameManager.Instance.GetCurrMap().GetMapName())
                map.SetActive(true);
        }
	}
}
