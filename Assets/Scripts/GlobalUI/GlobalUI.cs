using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalUI : MonoBehaviourSingletonPersistent<GlobalUI>
{
    //Confirmation Display
    GameObject confirmationDisplay;

    // Use this for initialization
    void Awake()
    {
        base.Awake();
        confirmationDisplay = GameObject.FindWithTag("ConfirmationDisplay");
        confirmationDisplay.GetComponent<ToggleActiveScript>().ToggleActive(false);
    }

    public void ToggleConfirmationDisplay(ListOfControllerActions controller, Button button, EXECUTE_ACTION action = EXECUTE_ACTION.NOTHING, bool playSound = true)
    {
        confirmationDisplay.GetComponent<ToggleActiveScript>().ToggleActive(playSound);
        if (confirmationDisplay.activeSelf)
        {
            confirmationDisplay.GetComponent<ConfirmationActionScript>().SetStateButtonAction(GameManager.Instance.GetGameState(), button, action);
            confirmationDisplay.GetComponent<ConfirmationDisplayScript>().SetControllerToReadFrom(controller);
            confirmationDisplay.GetComponent<ConfirmationDisplayScript>().Reset();
        }
    }

    public bool GetConfirmationDisplayActive()
    {
        return confirmationDisplay.activeSelf;
    }

}
