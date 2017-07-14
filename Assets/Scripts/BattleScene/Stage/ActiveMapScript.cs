using UnityEngine;
using System.Collections;

public class ActiveMapScript : MonoBehaviour 
{
    // Use this for initialization
	void Start () 
    {
        string activeMap = GameManager.Instance.GetCurrMap().GetMapName();
        DynamicFightingCameraScript cam = GameObject.FindWithTag("MainCamera").GetComponent<DynamicFightingCameraScript>();
        for(int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).name != activeMap)
                transform.GetChild(i).gameObject.SetActive(false);
            else
            {
                MapDataScript bound = transform.GetChild(i).GetComponent<MapDataScript>();// FindChild("Background").GetComponentInChildren<SpriteRenderer>().bounds;
                cam.SetMapBounds(bound);
                //cam.SetMapLengthHeight(bound.min, bound.max);
                //cam.SetMapLengthHeight();
            }

        }
    }
}
