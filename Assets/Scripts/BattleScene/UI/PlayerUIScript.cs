using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIScript : MonoBehaviour 
{
    public TEAM playerTeam;
    public GameObject hpObj;
    public GameObject manaObj;
    PlayerCharacterLogicScript playerInfo;
    Sprite charArtSprite;
    Image currHealth,currMana;

	// Use this for initialization
	void Start () {
        playerInfo = GameObject.FindWithTag("UserInterface").GetComponent<BattleSceneManager>().GetPlayerCharacter((int)playerTeam).GetComponent<PlayerCharacterLogicScript>();
        currHealth = hpObj.GetComponent<Image>();
        currMana = manaObj.GetComponent<Image>();
        GetComponent<Image>().sprite = playerInfo.GetCharacterData().GetCharArt();
    }
	
	// Update is called once per frame
	void Update () 
    {
        currHealth.fillAmount = playerInfo.GetHealthPercentage();
        currMana.fillAmount = playerInfo.percentageOfMana();
	}

}
