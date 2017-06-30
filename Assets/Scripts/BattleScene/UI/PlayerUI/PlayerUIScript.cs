using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIScript : MonoBehaviour 
{
    public TEAM playerTeam;
    public GameObject charArtObj;
    public GameObject hpObj;
    public GameObject manaObj;
    public GameObject ultibarObj;
    PlayerCharacterLogicScript playerInfo;
    Image charArt, currHealth, currMana, ultibar;
    public PlayerSkillsDisplayScript playerSkills;
    float ultAmountToReach = 0.0f;
    bool updateUltMeter = false;

    bool runOnce = true;

	// Use this for initialization
	void Start () 
    {
        playerInfo = GameObject.FindWithTag("UserInterface").GetComponent<BattleSceneManager>().GetPlayerCharacter((int)playerTeam).GetComponent<PlayerCharacterLogicScript>();
        currHealth = hpObj.GetComponent<Image>();
        currMana = manaObj.GetComponent<Image>();
        charArt = charArtObj.GetComponent<Image>();
        ultibar = ultibarObj.GetComponent<Image>();
        charArt.sprite = playerInfo.GetCharacterData().GetCharArt();

        ultAmountToReach = 0.0f;
        updateUltMeter = false;

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (runOnce)
        {
            playerSkills.SetSkillsIcon(playerInfo.GetSkillActivator());
            runOnce = false;
        }
        currHealth.fillAmount = playerInfo.GetHealthPercentage();
        currMana.fillAmount = playerInfo.percentageOfMana();

        ultAmountToReach = playerInfo.GetUltiPercentage();
        float ultcharge = (ultAmountToReach - ultibar.fillAmount);

        if (ultcharge > 0.0f)
            ultibar.fillAmount += 0.01f;
        else if(ultAmountToReach == 0.0f)
            ultibar.fillAmount = 0.0f;
	}
}
