using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPickScript : MonoBehaviour {

    Text display;
    public PLAYER player;
	// Use this for initialization
	void Start () {
        if (GameManager.Instance.GetGameMode() == GAME_MODES.LOCAL_PVP)
            StartCoroutine(LateStart(0.01f));
        else
            transform.parent.gameObject.SetActive(false);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        display = GetComponent<Text>();
        if (player == GameManager.Instance.GetCurRandomPlayer())
            display.text = "Player " + (int)(player + 1) + " Pick";
        else
            display.text = "Player " + (int)(player + 1) + " Wait";
    }
    
}
