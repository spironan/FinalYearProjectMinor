using UnityEngine;
using System.Collections;

public class StunMeter : MonoBehaviour
{

    [Range(0.01f, 100.0f)]
    public float stunValue;
    [Range(0.01f, 100.0f)]
    public float rateOfStunDecay;
    public GameObject border;
    public GameObject fill;


    private float maxStunValue = 100.0f;
    private bool stopStunValueDecrease = false;
    //// Use this for initialization
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (!stopStunValueDecrease)
        {
            //stunValue = Mathf.Clamp(stunValue, 0.1f, 100.0f);
            stunValue -= rateOfStunDecay * Time.deltaTime;
            stunValue = Mathf.Clamp(stunValue, 0.1f, maxStunValue);
            fill.transform.localScale = new Vector3(stunValue / 100, 1, 1);
        }
    }

    public float getStunValue()
    {
        return stunValue;
    }

    public float getMaxStunValue()
    {
        return maxStunValue;
    }

    public void setStunValue(float value)
    {
        stunValue = value;
    }

    public void addStunValue(float value)
    {
        stunValue += value;
    }

    public void setPosition(float x, float y, float z)
    {
        transform.position.Set(x, y, z);
    }

    public void setToStopStunDecrease(bool trigger)
    {
        stopStunValueDecrease = trigger;
    }
    //public void pauseStunDecrease()
    //{
    //    stopStunValueSecrease = true;
    //}

    //public void resumeStunDecrease()
    //{
    //    stopStunValueSecrease = false;
    //}
}
