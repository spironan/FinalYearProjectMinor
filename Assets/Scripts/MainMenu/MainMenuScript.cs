using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
    public enum MainStates
    {
        MainStatesBegin = 0,
        PlayerVPlayer = MainStatesBegin,
        Options,
        Exit,
        MainStatesEnd = Exit
    };

    public Vector3 offset = new Vector3(10,0,0);
    public GameObject cursor;
    public GameObject[] main;
    private MainStates currentState;
    bool stateChanged = false;
    void Start()
    {
        currentState = MainStates.MainStatesBegin;
        stateChanged = true;
        GameManager.Instance.ChangeState(GAMESTATE.MAIN_MENU);
    }

    void Update()
    {
        MoveUpDown();
        if (stateChanged)
        {
            cursor.transform.position = main[(int)currentState].transform.position + offset;
            Debug.Log("something changed , current State : " + currentState);
            stateChanged = false;
        }
        Select();

        //Debug.Log("Input Axis Y Value : " + Input.GetAxis("leftStick_Y_xBox360"));
    }

    private void Select()
    {
        if(Input.GetKeyDown(KeyCode.Space)
            ||Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter Has been Pressed");
        }
    }

    public void MoveUpDown()
    {
        MoveUpInput();
        MoveDownInput();
    }

    public void MoveUpInput()
    {
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetAxis("leftStick_Y_xBox360") > 0
            )
            MoveUp();
    }
    private void MoveUp()
    {
        currentState++;
        if (currentState > MainStates.MainStatesEnd)
            currentState = MainStates.MainStatesBegin;

        stateChanged = true;
    }
    
    public void MoveDownInput()
    {
        if (Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetAxis("leftStick_Y_xBox360") < 0
            )
            MoveDown();
    }
    private void MoveDown()
    {
        currentState--;
        if (currentState < MainStates.MainStatesBegin)
            currentState = MainStates.MainStatesEnd;

        stateChanged = true;
    }

    private void ChangeScene(string sceneName)
    {
    }

}

