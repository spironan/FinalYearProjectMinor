using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWinScript : MonoBehaviour 
{
    Text Wintext;

	// Use this for initialization
	void Start () {
        Wintext = GetComponent<Text>();
        DisplayPlayerVictory();
	}

    void DisplayPlayerVictory()
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            if (!gameManager.GetPlayer(i).GetInGameData().GetSetWon())
                continue;

            Wintext.text = "Player " + (i + 1) + " Win";
        }
    }

}
