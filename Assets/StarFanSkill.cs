using UnityEngine;
using System.Collections;

public class StarFanSkill : SkillProfile
{
    public GameObject ninjaStarskill;
    GameObject spawning_skill;
    PlayerControllerManager playerControllerManager;
    public override void Start()
    {
        //base.Start();
        transform.parent = owner.transform;
        playerControllerManager = owner.GetComponent<PlayerCharacterLogicScript>().GetController();
    }

    public override void Update()
    {
        for(int i = 0; i < 5; ++i)
        {
            spawning_skill = Instantiate(ninjaStarskill, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            spawning_skill.SetActive(true);
            spawning_skill.transform.position = transform.position;
            spawning_skill.GetComponent<SkillProfile>().offSetSpawn(owner.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
            spawning_skill.GetComponent<SkillProfile>().player_ID = playerControllerManager.playerID;
            spawning_skill.GetComponent<SkillProfile>().owner = owner;
            spawning_skill.GetComponent<SkillProfile>().enemy = enemy;
            if(i == 0)
            {
                //west
                spawning_skill.GetComponent<SkillProfile>().setDirection(new Vector2(-1, 0));
            }
            else if(i == 1)
            {
                //north west
                spawning_skill.GetComponent<SkillProfile>().setDirection(new Vector2(-0.5f, 0.5f));
            }
            else if (i == 2)
            {
                //north
                spawning_skill.GetComponent<SkillProfile>().setDirection(new Vector2(0, 1f));
            }
            else if (i == 3)
            {
                //north east
                spawning_skill.GetComponent<SkillProfile>().setDirection(new Vector2(0.5f, 0.5f));
            }
            else if (i == 4)
            {
                //north east
                spawning_skill.GetComponent<SkillProfile>().setDirection(new Vector2(1f, 0));
            }
        }
        //delete spawner
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
