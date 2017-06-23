using UnityEngine;
using System.Collections;

public class ResetCharSelectScript : MonoBehaviour {

    public void ResetCharacterSelect()
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < gameManager.GetPlayerSize(); ++i)
        {
            gameManager.GetPlayer(i).UnPickChar();
        }

    }
}
