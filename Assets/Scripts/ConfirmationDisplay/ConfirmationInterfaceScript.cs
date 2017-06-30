using UnityEngine;
using System.Collections;

public class ConfirmationInterfaceScript : MonoBehaviour 
{
    GameObject confirmationObj;

	// Use this for initialization
	void Start () {
        if (confirmationObj == null)
            confirmationObj = GameObject.FindWithTag("ConfirmationDisplay");//.GetComponent<ConfirmationDisplayScript>();

	}
	
    public void ActivateDisplay()
    {
        confirmationObj.GetComponent<ToggleActiveScript>().ToggleActive();

    }

	// Update is called once per frame
	void Update () {
	
	}
}
