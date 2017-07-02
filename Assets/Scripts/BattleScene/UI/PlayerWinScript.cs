using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWinScript : MonoBehaviour 
{
    Text Wintext;

	// Use this for initialization
	void Awake () {
        Wintext = GetComponent<Text>();
	}

    public void DisplayPlayerVictory()
    {
        for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
        {
            if (!GameManager.Instance.GetPlayer(i).GetInGameData().GetSetWon())
                continue;

            Wintext.text = "Player " + (i + 1) + " Win";
        }
    }

}
