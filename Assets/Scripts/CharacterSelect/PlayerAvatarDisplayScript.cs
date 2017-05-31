using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAvatarDisplayScript : MonoBehaviour 
{
    public int playerNumber;
    Sprite charaArt;
    Text charaName;
    CharacterSelectScript charSelect;
	// Use this for initialization
	void Start () {
        charSelect = GameObject.Find("CharacterSelect").GetComponent<CharacterSelectScript>();
        charaArt = GetComponent<Image>().sprite;
        charaName = GetComponentInChildren<Text>();//might not work
	}
	
	// Update is called once per frame
	void Update () {
        if (charaName.text != charSelect.GetCurrChara(playerNumber))
        {
            charaArt = charSelect.GetCharaArt(playerNumber);
            charaName.text = charSelect.GetCurrChara(playerNumber);
        }
	}
}
