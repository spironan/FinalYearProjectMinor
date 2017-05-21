using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapSelectScript : MonoBehaviour 
{
    GameObject gameManager;
    List<GameObject> maps = new List<GameObject>();
    public GameObject mapPrefab;

    public Vector3 startPos;
    public float itemWidth;
    public float spaceBetweenMaps;
    Vector3 offset;

    //public Vector3 centrePos;
    //public int radius;
    //public float timeToRotate;
    //public float cutOffZ;
    //float angularVelocity;
    //float rotationTime;
    //float shiftAngle;
    int currIndex = 0;
    int totalMaps = 0;
    float buttonCD;
    float buttonCurrCD;
    bool canActivate = true;

    bool mapPicked = false;
    bool cancelled = false;

    public Vector3 moveBy;
    Vector3 destination = Vector3.zero;
    Vector3 dir = Vector3.zero;
    public float speed = 2.0f;
    float timeToDest = 0.0f;
    bool atDest = true;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        
        //buttonCD = buttonCurrCD = timeToRotate;
        totalMaps = (int)PLAYMAPS.MAX_MAP;
        //shiftAngle = (1.0f / (totalMaps * 1.0f));
        //float circleProgress = 0.0f;
        //float angle,x, z;

        offset = new Vector3(itemWidth + spaceBetweenMaps, 0, 0);
        PLAYMAPS currMap = PLAYMAPS.MAPS_BEGIN;
        for (int i = 0; i < totalMaps; ++i)
        {
            //circleProgress = (i * 1.0f) / (totalMaps * 1.0f);
            //angle = circleProgress * Mathf.PI * 2;
            //x = Mathf.Sin(angle) * radius;
            //z = Mathf.Cos(angle) * radius;
            //Vector3 pos = new Vector3(x, 0, -z) + centrePos;
            Vector3 position = startPos + offset * i;

            GameObject map = Instantiate(mapPrefab);
            map.transform.SetParent(gameObject.transform, false);
            map.transform.localScale = new Vector3(1, 1, 1);
            map.transform.localPosition = position;
            //map.GetComponent<Image>().color = new Color(x, 0, z);
            //map.GetComponent<MapSlot>().SetCenter(centrePos);
            //map.GetComponent<MapSlot>().SetRadius(radius);
            //map.GetComponent<MapSlot>().SetCurrAngle(i * shiftAngle);
            map.GetComponent<MapSlot>().SetID(currMap);
            //map.GetComponent<MapSlot>().SetSpeed(currMap);
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

        //for (int i = 0; i < totalMaps; ++i)
        //{
        //    if (maps[i].transform.localPosition.z >= cutOffZ)
        //        maps[i].GetComponent<Image>().CrossFadeAlpha(0, 0.0f, false);
        //    else
        //        maps[i].GetComponent<Image>().CrossFadeAlpha(1, 0.0f, false);
        //}

        ///Controls here
        PlayerData player = gameManager.GetComponent<GameManager>().GetPlayer(0);
        if (player.controller.getAxisActionBoolDown(ACTIONS.MOVE_LEFT))
        {
            ShiftLeft(); // Shift The Things To The Left 
        }
        else if (player.controller.getAxisActionBoolDown(ACTIONS.MOVE_RIGHT))
        {
            ShiftRight(); // Shift The Things To The Right
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

    void FixedUpdate()
    {
        if (timeToDest > Time.deltaTime)
        {
            timeToDest -= Time.deltaTime;
            transform.position += dir * Time.deltaTime * speed;
        }
        else if (timeToDest <= Time.deltaTime && timeToDest > 0)
        {
            timeToDest = 0.0f;
            transform.position = destination;
            dir = Vector3.zero;
            atDest = true;
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

    public void SetCancel(bool cancel) { cancelled = cancel; }
    public void Cancel() { cancelled = true; }
    public bool CancelMapSelect() { return cancelled; }

    public void ShiftLeft()
    {
        Shift("Left");
    }

    public void ShiftRight()
    {
        Shift("Right");
    }

    void Shift(string direction)
    {
        if (canActivate)
        {
            canActivate = false;
            buttonCurrCD = 0.0f;
            switch (direction)
            {
                case "Left":
                    if (currIndex > 0)
                    {
                        destination = transform.position + moveBy;
                        atDest = false;
                        dir = (destination - transform.position).normalized;
                        timeToDest = (destination - transform.position).magnitude / speed;
                        buttonCD = timeToDest;
                        DecreaseIndex();
                    }
                    break;
                case "Right":
                    if (currIndex < totalMaps - 1)
                    {
                        destination = transform.position - moveBy;
                        atDest = false;
                        dir = (destination - transform.position).normalized;
                        timeToDest = (destination - transform.position).magnitude / speed;
                        buttonCD = timeToDest;
                        IncreaseIndex();
                    }
                    break;
            }

            //RotateByFixedAmount(-1);
            //for (int i = 0; i < totalMaps; ++i)
            //{
            //    int prev = i - 1;
            //    if (prev == -1)
            //        prev = totalMaps - 1;
            //    maps[i].GetComponent<MapSlot>().Move(maps[prev].transform.position);
            //    //maps[i].GetComponent<MapSlot>().Move(shiftAngle, timeToRotate);
            //}
            //transform.position = new Vector3(transform.position.x + amountToMove, transform.position.y, transform.position.z);

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
