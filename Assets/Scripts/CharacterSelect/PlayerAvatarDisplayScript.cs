using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAvatarDisplayScript : MonoBehaviour 
{
    public TEAM playerTeam;
    Image charaArt;
    Text charaName;
    CharacterSelectScript charSelect;

	// Use this for initialization
	void Start () 
    {
        charSelect = GameObject.Find("CharacterSelect").GetComponent<CharacterSelectScript>();
        charaArt = GetComponent<Image>();
        charaName = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        string currentChar = charSelect.GetCurrChara(playerTeam);
        if (currentChar != null)
            if (charaName.text != currentChar)
            {
                charaName.text = currentChar;
                charaArt.sprite = charSelect.GetCharaArt(playerTeam);
            }
	}

}
