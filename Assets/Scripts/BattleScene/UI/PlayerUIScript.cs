﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIScript : MonoBehaviour 
{
    public TEAM playerTeam;
    public GameObject hpObj;
    PlayerInGameData playerInfo;
    Sprite charArtSprite;
    Image currHealth;

	// Use this for initialization
	void Start () {
        playerInfo = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetPlayer(playerTeam).GetInGameData();
        charArtSprite = GetComponent<Image>().sprite;
        charArtSprite = playerInfo.GetCharData().GetCharArt();
        currHealth = hpObj.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        currHealth.fillAmount = playerInfo.GetHealthPercentage();
	}

}
