using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSlot : MonoBehaviour 
{
    public GameObject left = null, right = null;
    string mapName;

    public void SetMapName(string mapName) { this.mapName = mapName; }
    public string GetMapName() { return mapName; }

    //PLAYMAPS mapID;
    //public void SetID(PLAYMAPS mapID) { this.mapID = mapID; }
    //public void SetID(int mapID) { this.mapID = (PLAYMAPS)mapID; }
    //public int GetMapID() { return (int)mapID; }
    //public PLAYMAPS GetMapID() { return mapID; }

    //public Map map;
    //Vector3 centerPos;
    //float angleDif = 0.0f, currAngle = 0.0f, targetAngle = 0.0f;
    //float radius = 0.0f, durationToTarget = 0.0f, angularVel = 0.0f,countDownTimer = 0.0f;
    //Vector3 centrePos;
    //float defaultAngle = 0.0f;
    //float currAngle = 0.0f;
    //float angleToReach = 0.0f;
    //float angleToMove = 0.0f;
    //float angularVelocity = 0.0f;
    //float timeToRotate = 0.0f;
    //float radius;

    //void FixedUpdate()
    //{
    //    //if (!atDest)
    //    //{
    //    //    angularVel = (angleDif * Time.deltaTime) / durationToTarget;

    //    //    currAngle += angularVel;
    //    //    CheckOverUnderFlow();
    //    //    float x = Mathf.Sin(currAngle * Mathf.PI * 2) * radius;
    //    //    float z = Mathf.Cos(currAngle * Mathf.PI * 2) * radius;

    //    //    gameObject.transform.localPosition = centerPos + new Vector3(x, 0, -z);

    //    //    countDownTimer -= Time.deltaTime;
    //    //    if (Mathf.Abs(targetAngle - currAngle) < 0.001f || countDownTimer <= 0.0f)
    //    //    {
    //    //        countDownTimer = 0.0f;
    //    //        atDest = true;
    //    //    }
    //    //}
    //    //while (currAngle > 1.0f)
    //    //    currAngle -= 1.0f;
    //    //while(currAngle < -1.0f)
    //    //    currAngle += 1.0f;
    //    //while (angleToReach > 1.0f)
    //    //    angleToReach -= 1.0f;
    //    //while (angleToReach < -1.0f)
    //    //    angleToReach += 1.0f;

    //    //if (Mathf.Abs(currAngle - angleToReach) > 0.001f)
    //    //{
    //    //    angularVelocity = (angleToMove * Time.deltaTime) / timeToRotate;
    //    //    //Debug.Log("Angular Vel : " + angularVelocity);

    //    //    currAngle += angularVelocity;
    //    //    //Debug.Log("currAngle : " + currAngle);
    //    //    float x = Mathf.Sin(currAngle * Mathf.PI * 2) * radius;
    //    //    float z = Mathf.Cos(currAngle * Mathf.PI * 2) * radius;
    //    //    gameObject.transform.localPosition = centrePos + new Vector3(x, 0, -z);

    //    //    //if (angularVelocity > 0.0f)
    //    //    //{
    //    //    //    angleToMove -= angularVelocity;
    //    //    //    if (angleToMove < 0.0f)
    //    //    //        angleToMove = 0.0f;
    //    //    //}
    //    //    //else if (angularVelocity < 0.0f)
    //    //    //{
    //    //    //    angleToMove += angularVelocity;
    //    //    //    if (angleToMove > 0.0f)
    //    //    //        angleToMove = 0.0f;
    //    //    //}
    //    //    //timeToRotate -= Time.deltaTime;
    //    //    //if (timeToRotate < 0.0f)
    //    //    //    timeToRotate = 0.0f;
    //    //}
    //}

    ////void CheckOverUnderFlow()
    ////{
    ////    if (currAngle >= 1.0f)
    ////        currAngle -= 1.0f;
    ////    else if (currAngle <= -1.0f)
    ////        currAngle += 1.0f;

    ////    if (targetAngle >= 1.0f)
    ////        targetAngle -= 1.0f;
    ////    else if (targetAngle <= -1.0f)
    ////        targetAngle += 1.0f;
    ////}
    ////public void SetRadius(float newRadius)
    ////{
    ////    radius = newRadius;
    ////    Debug.Log("new radius is : " + radius);
    ////}

    ////public void SetCenter(Vector3 center)
    ////{
    ////    centerPos = center;
    ////}

    ////public void SetCurrAngle(float newAngle) 
    ////{
    ////    //defaultAngle = angleToReach = currAngle = newAngle; 
    ////    targetAngle = currAngle = newAngle;
    ////    //Debug.Log("currAngle : " + currAngle);
    ////}

    //public void Move(float angle, float rotateTime)
    //{
    //    //targetAngle = targetAngle + angle;
    //    //countDownTimer = durationToTarget = rotateTime;
    //    //angleDif = targetAngle - currAngle;
    //    //atDest = false;
    //    //Debug.Log("targetAngle : " + targetAngle + " currAngle : " + currAngle + " angleDif : " + angleDif);
    //    //CheckOverUnderFlow();
    //    //switch(direction)
    //    //{
    //    //    case "Left":
    //    //        //angleToMove -= angle;
    //    //        //timeToRotate += rotateTime;
    //    //        //angleToReach = defaultAngle + angleToMove;
    //    //    break;
    //    //    case "Right":
    //    //        //angleToMove += angle;
    //    //        //timeToRotate += rotateTime;
    //    //        //angleToReach = defaultAngle + angleToMove;
    //    //    break;
    //    //}

    //    //while (angleToMove > 1.0f)
    //    //    angleToMove -= 1.0f;
    //    //while (angleToMove < -1.0f)
    //    //    angleToMove += 1.0f;
    //}
}
