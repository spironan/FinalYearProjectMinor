using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionButtonChanger : MonoBehaviour {

    public GameObject confirmButton;
    public GameObject backButton;
    public Sprite confirmPS4;
    public Sprite confirmXBOX360;
    public Sprite backPS4;
    public Sprite backXBOX360;
    public ControllerInput PS4;
    public ControllerInput XBOX360;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.GetMasterPlayerData().controller.GetControllerManager().currController == PS4)
        {
            if(confirmButton.GetComponent<Image>().sprite != confirmPS4
                && backButton.GetComponent<Image>().sprite != backPS4)
            {
                confirmButton.GetComponent<Image>().sprite = confirmPS4;
                backButton.GetComponent<Image>().sprite = backPS4;
            }
                
        }
        else
        {
            if (confirmButton.GetComponent<Image>().sprite != confirmXBOX360
                && backButton.GetComponent<Image>().sprite != backXBOX360)
            {
                confirmButton.GetComponent<Image>().sprite = confirmXBOX360;
                backButton.GetComponent<Image>().sprite = backXBOX360;
            }
            
        }
    }
}
