using UnityEngine;
using System.Collections;

public class urgentChangeScene : MonoBehaviour {


    GameObject gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().GetPlayerSize() > 0
            //&& gameManager.GetComponent<GameManager>().GetPlayer(PLAYER.PLAYER_ONE).IsKeyDown(BUTTON_INPUT.X)
            )
        { 
            //Debug.Log(gameManager.GetComponent<GameManager>().GetPlayer(PLAYER.PLAYER_ONE).IsKeyDown(BUTTON_INPUT.START));
            LoadingScreenManager.LoadScene("CharacterSelectScene");
        }

	}
}
