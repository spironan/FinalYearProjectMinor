using UnityEngine;
using System.Collections;

public class DynamicFightingCameraScript : MonoBehaviour
{
    public float InterfacePercentage; // percentage of the screen that is ui a value from 0 - 1
    public float nearestZoom;
    float mapLength, mapHeight;
    float floor, ceiling;
    float leftBorder, rightBorder;
    float maxOrthoSize;
    Vector2 mapCenter;

    float extraUIOrtho;
    Camera thisCam;
    float newOrthoSize;
    float camX, camY;
    
    GameObject[] players;
    Vector3 p1pos;
    Vector3 p2pos;

    void Start()
    {
        thisCam = GetComponent<Camera>();
        players = GameObject.FindGameObjectsWithTag("Player");
        InterfacePercentage = Mathf.Clamp01(InterfacePercentage);
        extraUIOrtho = InterfacePercentage;
    }

    public void SetMapBounds(MapDataScript mapData)
    {
        this.leftBorder = mapData.leftBorder;
        this.rightBorder = mapData.rightBorder;
        this.floor = mapData.floor;
        this.ceiling = mapData.ceiling;
        mapLength = rightBorder - leftBorder;
        mapHeight = ceiling - floor;
        if(mapLength > mapHeight)
            maxOrthoSize = mapLength / 2.0f;
        else
            maxOrthoSize = mapHeight / 2.0f;
        mapCenter = new Vector3((leftBorder + rightBorder) / 2.0f, (floor + ceiling) / 2.0f, 0);
    }

    //public void SetMapLengthHeight(Vector3 minPoint, Vector3 maxPoint)
    //{
    //    mapCenter = (minPoint + maxPoint) / 2.0f;
    //    mapLength = maxPoint.x - minPoint.x;
    //    mapHeight = maxPoint.y - minPoint.y;
    //    maxOrthoSize = mapHeight / 2.0f;

    //    leftBorder = mapCenter.x - maxOrthoSize + nearestZoom;
    //    rightBorder = mapCenter.x + maxOrthoSize - nearestZoom;
    //    floor = mapCenter.y - maxOrthoSize + nearestZoom;
    //    ceiling = mapCenter.y + maxOrthoSize - nearestZoom;
    //}

    //public void SetMapLengthHeight(Vector3 center, Vector3 size)
    //{
    //    this.mapLength = size.x;
    //    this.mapHeight = size.y;
    //    maxOrthoSize = mapHeight / 2.0f;
    //    mapCenter = center;
    //    leftBorder = center.x - maxOrthoSize + nearestZoom;
    //    rightBorder = center.x + maxOrthoSize - nearestZoom;
    //    floor = center.y - maxOrthoSize + nearestZoom;
    //    ceiling = center.y + maxOrthoSize - nearestZoom;
    //    Debug.Log("floor :" + floor + " ceiling :" + ceiling);
    //    Debug.Log("Camera Length :  " + mapLength + " Camera Height :  " + mapHeight);
    //    Debug.Log("Max Ortho Size : " + maxOrthoSize + " Map Center is " + mapCenter);
    //}

    void Update()
    {
        GetPlayerLocation();

        UpdateXCoord();
        UpdateOrthoSize();
        UpdateYCoord();

        transform.position = new Vector3(camX, camY, transform.position.z);
    }

    void GetPlayerLocation()
    {
        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");

        p1pos = players[0].transform.position;
        p2pos = players[1].transform.position;
    }

    void UpdateXCoord()
    {
        camX = (p1pos.x + p2pos.x) / 2.0f;
        camX = Mathf.Clamp(camX, leftBorder, rightBorder);
    }

    void UpdateOrthoSize()
    {
        float newOrthoSizeBasedOnX = Mathf.Abs(p1pos.x - p2pos.x)/2.0f;
        float newOrthoSizeBasedOnY = Mathf.Abs(p1pos.y - p2pos.y);

        newOrthoSize = Mathf.Max(newOrthoSizeBasedOnX, newOrthoSizeBasedOnY) + extraUIOrtho;
        newOrthoSize = Mathf.Clamp(newOrthoSize, nearestZoom, maxOrthoSize);
        thisCam.orthographicSize = newOrthoSize;
    }


    void UpdateYCoord()
    {
        //camY = (floor + ceiling) / 2.0f;
        camY = (p1pos.y + p2pos.y) / 2.0f;
        //float avgYPlayerPos = (p1pos.y + p2pos.y) / 2.0f;
        //if (Mathf.Abs(camY - avgYPlayerPos) > thisCam.orthographicSize)
        //{
        //    camY = thisCam.transform.position.y;

        //    //if (avgYPlayerPos < camY)
        //    //    camY = avgYPlayerPos - thisCam.orthographicSize;
        //    //else if (avgYPlayerPos > camY)
        //    //    camY = avgYPlayerPos + thisCam.orthographicSize;
        //}

        //if (avgYPlayerPos + thisCam.orthographicSize ==  )
        //camY = (p1pos.y + p2pos.y) / 2.0f;
        camY = Mathf.Clamp(camY, floor + thisCam.orthographicSize, ceiling);
    }

}
