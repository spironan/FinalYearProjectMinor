using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpDisplayScript : MonoBehaviour 
{
    public Image player1Help, player2Help;

	// Use this for initialization
	void Start () {
        player1Help.enabled = true;
        player2Help.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (player1Help.transform.parent.GetComponent<Image>().sprite.name != "Transparent" && player1Help.enabled)
        {
            player1Help.enabled = false;
            player2Help.enabled = true;
        }
        if (player2Help.transform.parent.GetComponent<Image>().sprite.name != "Transparent" && player2Help.enabled)
        {
            player2Help.enabled = false;
        }
	}
}
