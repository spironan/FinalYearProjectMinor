using UnityEngine;
using System.Collections;

public class PlayerSkillsDisplayScript : MonoBehaviour 
{
    SkillDisplayBaseScript[] skills;
	// Use this for initialization
	void Awake () {
        skills = GetComponentsInChildren<SkillDisplayBaseScript>();
	}

    public void SetSkillsIcon(SkillActivator activator)
    {
        //foreach (SkillDisplayBaseScript skill in skills)
        //{
        //    skill.SetIcon(activator.skil);
        //}
        skills[0].SetIcon(activator.skill1.GetComponent<SkillProfile>());
        skills[1].SetIcon(activator.skill2.GetComponent<SkillProfile>());
        skills[2].SetIcon(activator.skill3.GetComponent<SkillProfile>());
        skills[3].SetIcon(activator.skill4.GetComponent<SkillProfile>());
        switch(activator.playerControllerManager.currController.getControllerName())
        {
            case "XBOX360":
                skills[0].SetInput(SpriteManager.GetInstance().GetSprite("XBOX_X"));
                skills[1].SetInput(SpriteManager.GetInstance().GetSprite("XBOX_Y"));
                skills[2].SetInput(SpriteManager.GetInstance().GetSprite("XBOX_B"));
                skills[3].SetInput(SpriteManager.GetInstance().GetSprite("XBOX_A"));
                break;

            case "PS4":
                skills[0].SetInput(SpriteManager.GetInstance().GetSprite("PS4_Square"));
                skills[1].SetInput(SpriteManager.GetInstance().GetSprite("PS4_Triangle"));
                skills[2].SetInput(SpriteManager.GetInstance().GetSprite("PS4_Circle"));
                skills[3].SetInput(SpriteManager.GetInstance().GetSprite("PS4_Cross"));
                break;
        }
    }

}
