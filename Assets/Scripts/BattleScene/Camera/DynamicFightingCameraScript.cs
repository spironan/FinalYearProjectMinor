using UnityEngine;
using System.Collections;

public class DynamicFightingCameraScript : MonoBehaviour
{
    public float nearestZoom;
    float mapLength, mapHeight;
    float floor, ceiling;
    float leftBorder, rightBorder;
    float maxOrthoSize;
    Vector2 mapCenter;
    
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
    }

    public void SetMapLengthHeight(Vector3 center, Vector3 size)
    {
        this.mapLength = size.x;
        this.mapHeight = size.y;
        maxOrthoSize = mapHeight / 2.0f;
        mapCenter = center;
        leftBorder = center.x - maxOrthoSize + nearestZoom;
        rightBorder = center.x + maxOrthoSize - nearestZoom;
        floor = center.y - maxOrthoSize + nearestZoom;
        ceiling = center.y + maxOrthoSize - nearestZoom;

        Debug.Log("floor :" + floor + " ceiling :" + ceiling);
        Debug.Log("Camera Length :  " + mapLength + " Camera Height :  " + mapHeight);
        Debug.Log("Max Ortho Size : " + maxOrthoSize + " Map Center is " + mapCenter);
    }

    void Update()
    {
        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");

        p1pos = players[0].transform.position;
        p2pos = players[1].transform.position;

        camX = (p1pos.x + p2pos.x) / 2.0f;
        camX= Mathf.Clamp(camX, leftBorder, rightBorder);

        camY = (p1pos.y + p2pos.y) * 0.60f;/// 2.0f;
        camY = Mathf.Clamp(camY, floor, ceiling);

        newOrthoSize = Vector2.Distance(p1pos, p2pos) / 2.0f;
        newOrthoSize = Mathf.Clamp(newOrthoSize, nearestZoom, maxOrthoSize);

        thisCam.orthographicSize = newOrthoSize;
        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
