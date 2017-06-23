using UnityEngine;
using System.Collections;

public class ToggleActiveScript : MonoBehaviour 
{
    public bool defaultActive = true;
	// Use this for initialization
    void Start()
    {
        SetActive();
	}

    public void ToggleActive()
    {
        defaultActive = !defaultActive;
        SetActive();
    }

    public void ToggleActiveOn()
    {
        defaultActive = true;
        SetActive();
    }

    public void ToggleActiveOff()
    {
        defaultActive = false;
        SetActive();
    }

    void SetActive()
    {
        gameObject.SetActive(defaultActive);
    }
}
