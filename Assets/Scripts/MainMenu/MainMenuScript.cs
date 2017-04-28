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
    static bool canInteract;
    void Start()
    {
        currentState = MainStates.MainStatesBegin;
        stateChanged = true;
        canInteract = true;
    }

    void OnGUI()
    {
        
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
            || Input.GetAxis("LeftJoyStickVertical") > 0 && canInteract
            )
            MoveUp();
    }
    private void MoveUp()
    {
        currentState++;
        if (currentState > MainStates.MainStatesEnd)
            currentState = MainStates.MainStatesBegin;

        stateChanged = true;
        canInteract = false;
    }
    
    public void MoveDownInput()
    {
        if (Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetAxis("LeftJoyStickVertical") < 0 && canInteract
            )
            MoveDown();
    }
    private void MoveDown()
    {
        currentState--;
        if (currentState < MainStates.MainStatesBegin)
            currentState = MainStates.MainStatesEnd;

        stateChanged = true;
        canInteract = false;
    }

    private void ChangeScene(string sceneName)
    {

    }

}

