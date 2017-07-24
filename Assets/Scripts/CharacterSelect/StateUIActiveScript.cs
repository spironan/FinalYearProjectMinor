using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateUIActiveScript : MonoBehaviour {

    public List<GAME_MODES> inactiveModes;

	// Use this for initialization
	void Start () {
        foreach (GAME_MODES mode in inactiveModes)
            if (GameManager.Instance.GetGameMode() == mode)
            {
                gameObject.SetActive(false);
                break;
            }
	}
	
}
