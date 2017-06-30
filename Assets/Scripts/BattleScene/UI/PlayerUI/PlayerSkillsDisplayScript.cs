using UnityEngine;
using System.Collections;

public class PlayerSkillsDisplayScript : MonoBehaviour 
{
    SkillDisplayBaseScript[] skills;
    bool runOnce = true;
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
        skills[0].SetIcon(activator.skill1.GetComponent<SkillProfile>().skillIcon);
        skills[1].SetIcon(activator.skill2.GetComponent<SkillProfile>().skillIcon);
        skills[2].SetIcon(activator.skill3.GetComponent<SkillProfile>().skillIcon);
        skills[3].SetIcon(activator.skill4.GetComponent<SkillProfile>().skillIcon);
    }

	// Update is called once per frame
	void Update () {
        
	}
}
