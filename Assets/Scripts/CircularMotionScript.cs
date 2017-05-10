using UnityEngine;
using System.Collections;

public class CircularMotionScript : MonoBehaviour 
{
    public bool rotateX, rotateY, rotateZ;
    public bool runOnUpdate;
    public float speed, width, height;
    float TimePassed;
    Vector3 startPos;

	// Use this for initialization
	void Start () 
    {
        rotateX = rotateY = rotateZ = true;
        runOnUpdate = false;
        startPos = transform.position;
        TimePassed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (runOnUpdate)
        {
            TimePassed += Time.deltaTime * speed;
            Rotate();
        }
	}

    void Rotate()
    {
        float x, y, z;
        x = y = z = 0;
        if(rotateX)
            x = Mathf.Cos(TimePassed) * width;
        if(rotateY)
            y = Mathf.Sin(TimePassed) * height;
        if(rotateZ)
            z = 0;
        transform.position = new Vector3(x, y, z) + startPos;
    }

    public void RotateByFixedAmount(float amount)
    {
        float x, y, z;
        x = y = z = 0;
        if (rotateX)
            x = Mathf.Cos(amount) * width;
        if (rotateY)
            y = Mathf.Sin(amount) * height;
        if (rotateZ)
            z = 0;
        transform.position = new Vector3(x, y, z) + startPos;
    }
}
