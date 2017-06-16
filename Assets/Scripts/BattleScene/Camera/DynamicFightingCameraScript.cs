using UnityEngine;
using System.Collections;

public class DynamicFightingCameraScript : MonoBehaviour 
{
    public float nearestZoom;
    public float furthestZoom;
    GameObject[] players;
    Vector3 p1pos;
    Vector3 p2pos;
    Vector3 camOriginalPos;
    Camera thisCam;
    float newOrthoSize;
    float camX;         
    float camY;

	void Start ()
    {
        thisCam = GetComponent<Camera>();
        players = GameObject.FindGameObjectsWithTag("Player");
        camOriginalPos = transform.position;
	}
	
	void Update ()
    {
        if(players == null)
            players = GameObject.FindGameObjectsWithTag("Player");
        p1pos = players[0].transform.position;
        p2pos = players[1].transform.position;

        newOrthoSize = Vector2.Distance(p1pos, p2pos)/2.0f;
        //If Too Small
        if (newOrthoSize < nearestZoom)
            thisCam.orthographicSize = nearestZoom;
        //Else if Too Big
        else if (newOrthoSize > furthestZoom)
            thisCam.orthographicSize = furthestZoom;
        //Else Just Nice
        else
            thisCam.orthographicSize = newOrthoSize;

        camX = (p1pos.x + p2pos.x) / 2.0f;
        camY = (p1pos.y + p2pos.y) / 2.0f + thisCam.orthographicSize / 4.0f;
        transform.position = new Vector3(camX, camY, transform.position.z);
        
	}   
}
