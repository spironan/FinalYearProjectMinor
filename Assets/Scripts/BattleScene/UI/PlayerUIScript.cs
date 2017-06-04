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
        playerInfo = GameObject.FindWithTag("BattleSceneManager").GetComponent<BattleSceneManager>().GetPlayerCharacter((int)playerTeam).GetComponent<PlayerCharacterLogicScript>();//GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetPlayer(playerTeam).GetInGameData();
        GetComponent<Image>().sprite = playerInfo.GetCharacterData().GetCharArt();
        Debug.Log("Added Character Art Image to UI");
        currHealth = hpObj.GetComponent<Image>();
        currMana = manaObj.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        currHealth.fillAmount = playerInfo.GetHealthPercentage();
        currMana.fillAmount = playerInfo.percentageOfMana();
	}

}
