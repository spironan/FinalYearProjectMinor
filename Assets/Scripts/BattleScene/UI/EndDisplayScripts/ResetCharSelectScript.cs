using UnityEngine;
using System.Collections;

public class ResetCharSelectScript : MonoBehaviour {

    public void ResetCharacterSelect()
    {
        for (int i = 0; i < GameManager.Instance.GetPlayerSize(); ++i)
        {
            GameManager.Instance.GetPlayer(i).UnPickChar();
        }
    }
}
