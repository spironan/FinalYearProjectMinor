using UnityEngine;
using System.Collections;

public class FloatAndBool {

    private float value;
    private bool trueFalse;

    public FloatAndBool()
    {
    }
    public FloatAndBool(float floatingPoint,bool boolean)
    {
        value = floatingPoint;
        trueFalse = boolean;
    }

    public void setFloatValue(float floatingPoint)
    {
        value = floatingPoint;
    }
    public void setBool(bool boolean)
    {
        trueFalse = boolean;
    }
    public void setFloatAndBool(float floatingPoint, bool boolean)
    {
        value = floatingPoint;
        trueFalse = boolean;
    }

    public float getFloat()
    {
        return value;
    }

    public bool getBool()
    {
        return trueFalse;
    }
}
