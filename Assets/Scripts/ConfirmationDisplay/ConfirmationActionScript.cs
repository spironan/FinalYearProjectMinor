using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public enum EXECUTE_ACTION
{
    NOTHING,
    BACK_TO_MAIN,
    BACK_TO_CHARSELECT,
    BACK_TO_MAPSELECT,
};

public class ConfirmationActionScript : MonoBehaviour 
{
    public Text displayText;

    EXECUTE_ACTION currAction = EXECUTE_ACTION.NOTHING;
    GAMESTATE currState;
    Button currentButton;
    PointerEventData pointer;

    //Function to be called by button to run the appropriate Action at the appropriate points
    public void ExecuteEvent()
    {
        if (currentButton != null)
        {
            pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
            ExecuteEvents.Execute(currentButton.gameObject, pointer, ExecuteEvents.submitHandler);
        }
    }

    //Function to be executed when you close the button
    public void ExecuteClose()
    {
        if (currentButton != null)
        {
            currentButton.Select();
        }
    }

    //Set What point the Game is at to determine what to if player Selects "Yes";
    public void SetAction(EXECUTE_ACTION action)
    {
        if (currAction != action)
            currAction = action;

        UpdateText();
    }

    public void SetState(GAMESTATE gameState)
    {
        if(currState != gameState)
            currState = gameState;
    }

    public void SetButton(Button newButton)
    {
        if (currentButton != newButton)
            currentButton = newButton;
    }

    public void SetStateButtonAction(GAMESTATE gameState, Button newButton, EXECUTE_ACTION action)
    {
        SetState(gameState);
        SetButton(newButton);
        SetAction(action);
    }

    void UpdateText()
    {
        switch (currAction)
        {
            case EXECUTE_ACTION.BACK_TO_MAIN:
                {
                    displayText.text = "Head back To Main Menu?";
                };
                break;
            case EXECUTE_ACTION.BACK_TO_CHARSELECT:
                {
                    displayText.text = "Head back To Character Select?";
                };
                break;
            case EXECUTE_ACTION.BACK_TO_MAPSELECT:
                {
                    displayText.text = "Head back To Map Select?";
                };
                break;
        }
    }

}
