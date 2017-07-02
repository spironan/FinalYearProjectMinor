using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapSelectScript : MonoBehaviour
{
    //public Vector3 centrePos;
    //public int radius;
    //public float timeToRotate;
    //public float cutOffZ;
    //float angularVelocity;
    //float rotationTime;
    //float shiftAngle;
    //buttonCD = buttonCurrCD = timeToRotate;
    //shiftAngle = (1.0f / (totalMaps * 1.0f));
    //float circleProgress = 0.0f;
    //float angle,x, z;
    //circleProgress = (i * 1.0f) / (totalMaps * 1.0f);
    //angle = circleProgress * Mathf.PI * 2;
    //x = Mathf.Sin(angle) * radius;
    //z = Mathf.Cos(angle) * radius;
    //Vector3 pos = new Vector3(x, 0, -z) + centrePos;
    //map.GetComponent<Image>().color = new Color(x, 0, z);
    //map.GetComponent<MapSlot>().SetCenter(centrePos);
    //map.GetComponent<MapSlot>().SetRadius(radius);
    //map.GetComponent<MapSlot>().SetCurrAngle(i * shiftAngle);
    //map.GetComponent<MapSlot>().SetSpeed(currMap);
    //currMap += 1;
    //map manager get instance get map icon 
    //for (int i = 0; i < totalMaps; ++i)
    //{
    //    if (maps[i].transform.localPosition.z >= cutOffZ)
    //        maps[i].GetComponent<Image>().CrossFadeAlpha(0, 0.0f, false);
    //    else
    //        maps[i].GetComponent<Image>().CrossFadeAlpha(1, 0.0f, false);
    //}
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

    public float itemWidth;
    public float spaceBetweenMaps;
    public float speed = 2.0f;
    public Vector3 startPos;

    GameObject mapPrefab;
    int currIndex = 0;
    int totalMaps = 0;
    float buttonCD;
    float buttonCurrCD;
    float timeToDest = 0.0f;
    bool canActivate = true;
    bool mapPicked = false;
    bool cancelled = false;
    bool atDest = true;

    List<GameObject> maps = new List<GameObject>();
    Vector3 offset;
    Vector3 moveBy;
    Vector3 destination = Vector3.zero;
    Vector3 dir = Vector3.zero;
    AudioClip select;

	// Use this for initialization
	void Start ()
    {
        select = AudioClipManager.GetInstance().GetAudioClip("Select");
        mapPrefab = PrefabManager.GetInstance().GetPrefab("MapSlot");
        totalMaps = MapManager.GetInstance().GetMapCount();
        offset = new Vector3(itemWidth + spaceBetweenMaps, 0, 0);

        for (int i = 0; i < totalMaps; ++i)
        {
            Vector3 position = startPos + offset * i;

            GameObject map = Instantiate(mapPrefab);
            map.transform.SetParent(gameObject.transform, false);
            map.transform.localScale = new Vector3(1, 1, 1);
            map.transform.localPosition = position;
            map.GetComponent<MapSlot>().SetMapName(MapManager.GetInstance().GetMapByIndex(i).GetMapName());
            map.GetComponent<MapSlot>().SetMapSprite(MapManager.GetInstance().GetMapByIndex(i).GetMapSprite());
            maps.Add(map);
        }

        // Move map
        if (totalMaps > 1)
        {
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
        }

        moveBy = new Vector3((itemWidth + spaceBetweenMaps)/619.195f,0,0);
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

        ///Controls here
        PlayerData player = GameManager.Instance.GetPlayer(0);
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
            GameObject.FindWithTag("CharacterSelect").GetComponent<CharacterSelectScript>().DeselectCharacter(player.GetInGameData().GetTeam());
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
        GameManager.Instance.SetCurrMap(maps[currIndex].GetComponent<MapSlot>().GetMapName());
        SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, AudioClipManager.GetInstance().GetAudioClip("PickMap"));
        mapPicked = true;
    }
    public bool CheckMapPicked()
    {
        return mapPicked;
    }

    public void SetCancel(bool cancel) { cancelled = cancel; }
    public void Cancel() { cancelled = true; }
    public bool CancelMapSelect() { return cancelled; }
    public string GetCurrentMapName() { return MapManager.GetInstance().GetMapByIndex(currIndex).GetMapName(); }

    public void ShiftLeft() { Shift("Left"); }
    public void ShiftRight() { Shift("Right"); }
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
            SoundSystem.Instance.PlayClip(AUDIO_TYPE.SOUND_EFFECTS, select);
            ResizeMaps();
        }
    }

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
