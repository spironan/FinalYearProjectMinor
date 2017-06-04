using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAvatarDisplayScript : MonoBehaviour 
{
    public int playerNumber;
    Image charaArt;
    Text charaName;
    CharacterSelectScript charSelect;

	// Use this for initialization
	void Start () 
    {
        charSelect = GameObject.Find("CharacterSelect").GetComponent<CharacterSelectScript>();
        charaArt = GetComponent<Image>();
        charaName = GetComponentInChildren<Text>();//might not work
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (charaName.text != charSelect.GetCurrChara(playerNumber))
        {
            charaName.text = charSelect.GetCurrChara(playerNumber);
            charaArt.sprite = charSelect.GetCharaArt(playerNumber);
        }
	}

}
