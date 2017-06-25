using UnityEngine;
using System.Collections;

public class ListOfAvailableSkills : MonoBehaviour {

    public SkillList skillList;

    SkillActivator skillActivator;
    BasicAttack basicAttack;

    private void Start()
    {
        //skillList will be loaded next time with database
        //skillList
        skillActivator = GetComponent<SkillActivator>();
        basicAttack = GetComponent<BasicAttack>();
        skillActivator.skillULTIMATE = skillList.ListOfSkills[0];

        skillActivator.skill1 = skillList.ListOfSkills[1];
        skillActivator.skill2 = skillList.ListOfSkills[2];
        skillActivator.skill3 = skillList.ListOfSkills[3];
        skillActivator.skill4 = skillList.ListOfSkills[4];

        basicAttack.skill = skillList.ListOfSkills[5];
        basicAttack.skill_cooldown = basicAttack.skill.GetComponent<SkillProfile>().castingCooldown;
    }

}
