using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapSelectScript : MonoBehaviour 
{
    GameObject gameManager;
    List<GameObject> maps = new List<GameObject>();
    public GameObject mapPrefab;
    public Vector3 centrePos;
    public int radius;
    public float timeToRotate;
    public float cutOffZ;
    int currIndex = 0;
    int totalMaps = 0;
    float angularVelocity;
    float rotationTime;
    float shiftAngle;
    bool mapPicked = false;
    float buttonCD;
    float buttonCurrCD;
    bool canActivate = true;
    bool cancelled = true;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        buttonCD = buttonCurrCD = timeToRotate;
        totalMaps = (int)PLAYMAPS.MAX_MAP;
        shiftAngle = (1.0f / (totalMaps * 1.0f));
        float circleProgress = 0.0f;
        float angle,x, z;

        PLAYMAPS currMap = PLAYMAPS.MAPS_BEGIN;
        for (int i = 0; i < totalMaps; ++i)
        {
            circleProgress = (i * 1.0f) / (totalMaps * 1.0f);
            angle = circleProgress * Mathf.PI * 2;
            x = Mathf.Sin(angle) * radius;
            z = Mathf.Cos(angle) * radius;
            Vector3 pos = new Vector3(x, 0, -z) + centrePos;

            GameObject map = Instantiate(mapPrefab);
            map.transform.SetParent(gameObject.transform, false);
            //map.transform.parent = gameObject.transform;
            map.transform.localScale = new Vector3(1, 1, 1);
            map.transform.localPosition = pos;
            map.GetComponent<Image>().color = new Color(x, 0, z);
            map.GetComponent<MapSlot>().SetCenter(centrePos);
            map.GetComponent<MapSlot>().SetRadius(radius);
            map.GetComponent<MapSlot>().SetCurrAngle(i * shiftAngle);
            map.GetComponent<MapSlot>().SetID(currMap);
            currMap += 1;
            //map manager get instance get map icon 
            maps.Add(map);
        }

        // Move map
        MapSlot tempMap;
        for (int i = 0; i < totalMaps; ++i)
        {
            tempMap = maps[i].GetComponent<MapSlot>();

            int left = i - 1;
            int right = i + 1;

            if (left < 0)
                left = totalMaps - 1;
            else if (right > totalMaps - 1)
                right = 0;

            tempMap.left = maps[left];
            tempMap.right = maps[right];
        }

        //Increase the size of the currentMap Slightly
        ResizeMaps();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (buttonCurrCD <= buttonCD)
            buttonCurrCD += Time.deltaTime;
        else if (buttonCurrCD > buttonCD)
            canActivate = true;

        for (int i = 0; i < totalMaps; ++i)
        {
            if (maps[i].transform.localPosition.z >= cutOffZ)
                maps[i].GetComponent<Image>().CrossFadeAlpha(0, 0.0f, false);
            else
                maps[i].GetComponent<Image>().CrossFadeAlpha(1, 0.0f, false);
        }

        ///Controls here
        ///
        PlayerData player = gameManager.GetComponent<GameManager>().GetPlayer(0);
        if (player.controller.getAxisActionBool(ACTIONS.MOVE_LEFT))
        {
            ShiftLeft();
        }
        else if (player.controller.getAxisActionBool(ACTIONS.MOVE_RIGHT))
        {
            ShiftRight();
        }
        if (player.controller.getButtonAction(ACTIONS.SELECT_MAP))
        {
            PickSelectedMap();
        }
        else if (player.controller.getButtonAction(ACTIONS.CANCEL_MAP_SELECT))
        {
            Cancel();
        }
	}

    public void PickSelectedMap()
    {
        gameManager.GetComponent<GameManager>().SetCurrMap(maps[currIndex].GetComponent<MapSlot>().GetMapID());
        mapPicked = true;
    }

    public bool CheckMapPicked()
    {
        return mapPicked;
    }

    public void Cancel() { cancelled = true; }

    public bool CancelMapSelect() { return cancelled; }

    public void ShiftLeft()
    {
        if (canActivate)
        {
            canActivate = false;
            buttonCurrCD = 0.0f;
            IncreaseIndex();
            //RotateByFixedAmount(1);
            for (int i = 0; i < totalMaps; ++i)
            {
                maps[i].GetComponent<MapSlot>().Move(-shiftAngle, timeToRotate);
            }
            ResizeMaps();
        }
    }

    public void ShiftRight()
    {
        if (canActivate)
        {
            canActivate = false;
            buttonCurrCD = 0.0f;
            DecreaseIndex();
            //RotateByFixedAmount(-1);
            for (int i = 0; i < totalMaps; ++i)
            {
                maps[i].GetComponent<MapSlot>().Move(shiftAngle, timeToRotate);
            }
            ResizeMaps();
        }
    }

    //void RotateByFixedAmount(float amount)
    //{
    //    float circleProgress = 0.0f;
    //    int index = 0;
    //    for (int i = 0; i < totalMaps; ++i)
    //    {
    //        circleProgress = (i * 1.0f) / (totalMaps * 1.0f);//0 - 1
    //        float angle = circleProgress * Mathf.PI * 2;
    //        float x = Mathf.Sin(angle) * radius;
    //        float z = Mathf.Cos(angle) * radius;
    //        Vector3 pos = new Vector3(x, 0, -z) + centrePos;

    //        index = currIndex + i;
    //        if (index >= totalMaps)
    //            index -= totalMaps;

    //        maps[index].transform.localPosition = pos;

    //        //if (amount > 0)
    //        //    maps[index].GetComponent<MapSlot>().MoveLeft();
    //        //else
    //        //    maps[index].GetComponent<MapSlot>().MoveRight();

    //        //if (maps[index].transform.localPosition.z >= cutOffZ)
    //        //    maps[index].SetActive(false);
    //        //else
    //        //    maps[index].SetActive(true);
    //    }
    //    maps[currIndex].transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
    //}

    void IncreaseIndex()
    {
        currIndex++;
        if (currIndex >= totalMaps)
            currIndex -= totalMaps;
    }
    
    void DecreaseIndex()
    {
        currIndex--;
        if (currIndex < 0)
            currIndex = totalMaps - 1;
    }

    void ResizeMaps()
    {
        for (int i = 0; i < totalMaps; ++i)
        {
            if (i == currIndex)
                maps[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            else
                maps[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

}
