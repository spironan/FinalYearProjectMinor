using UnityEngine;
using System.Collections;

public class ActiveMapScript : MonoBehaviour 
{
    // Use this for initialization
	void Start () {
        string activeMap = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetCurrMap().GetMapName();
        for(int i = 0; i < transform.childCount; ++i)
        {
            if(transform.GetChild(i).name != activeMap)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
