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
    //// Use this for initialization
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        
        //stunValue = Mathf.Clamp(stunValue, 0.1f, 100.0f);
        stunValue -= rateOfStunDecay * Time.deltaTime;
        stunValue = Mathf.Clamp(stunValue, 0.1f, 100.0f);
        fill.transform.localScale = new Vector3(stunValue / 100, 1, 1);
    }

    public float getStunValue()
    {
        return stunValue;
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
}
