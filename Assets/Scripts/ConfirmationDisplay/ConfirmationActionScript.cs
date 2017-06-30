using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    //Function to be called by button to run the appropriate Action at the appropriate points
    public void ExecuteEvent()
    {
        Debug.Log("Current State : " + currState);
        switch (currState)
        {
            case GAMESTATE.MAIN_MENU:
            {
            }
            break;
            case GAMESTATE.CHAR_SELECT:
            {
                if (currAction == EXECUTE_ACTION.BACK_TO_MAIN)
                {
                    LoadingScreenManager.LoadScene("MainMenuScene");
                }
            }
            break;
            case GAMESTATE.IN_GAME:
            {
                GameObject battleSceneObj = GameObject.FindWithTag("UserInterface");
                if (currAction == EXECUTE_ACTION.BACK_TO_MAIN)
                {
                    LoadingScreenManager.LoadScene("MainMenuScene");
                    battleSceneObj.GetComponent<BattleSceneManager>().ResetEntireSet();
                }
                else if (currAction == EXECUTE_ACTION.BACK_TO_CHARSELECT)
                {
                    LoadingScreenManager.LoadScene("CharacterSelectScene");
                    battleSceneObj.GetComponent<ResetCharSelectScript>().ResetCharacterSelect();
                    battleSceneObj.GetComponent<BattleSceneManager>().ResetEntireSet();
                }
                else if (currAction == EXECUTE_ACTION.BACK_TO_MAPSELECT)
                {
                    LoadingScreenManager.LoadScene("CharacterSelectScene");
                    battleSceneObj.GetComponent<BattleSceneManager>().ResetEntireSet();
                }
            }
            break;
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

    public void SetStateAndAction(GAMESTATE gameState, EXECUTE_ACTION action)
    {
        SetState(gameState);
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
