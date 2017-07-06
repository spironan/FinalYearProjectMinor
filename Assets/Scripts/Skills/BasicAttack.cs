using UnityEngine;
using System.Collections;

public class BasicAttack : MonoBehaviour {
    public GameObject skill;
    public float skill_cooldown = 0;

    float timer = 0;
    GameObject spawning_skill;
    PlayerControllerManager playerControllerManager;
    PlayerCharacterLogicScript owner;
    WordingsHolder wordingsHolder;
    // Use this for initialization
    void Start () {
        playerControllerManager = GetComponent<PlayerCharacterLogicScript>().GetController();
        wordingsHolder = GetComponent<WordingsHolder>();


    }
	
	// Update is called once per frame
	void Update () {
        //if(skill_cooldown == 0)
        //{
        //    skill_cooldown = skill.GetComponent<SkillProfile>().castingCooldown;
        //    timer = skill_cooldown;
        //}
        ////if (playerControllerManager == null)
        ////    playerControllerManager = GetComponent<PlayerControllerManager>();

        //timer += Time.deltaTime;
        //if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.R1)
        //    && timer >= skill_cooldown)
        //{
        //    timer = 0;
        //    if(owner == null)
        //        owner = GetComponent<PlayerCharacterLogicScript>();
            
            
        //    if (owner.getManaAmount() >= skill.GetComponent<SkillProfile>().manaCost)
        //    {
        //        owner.decreaseMana(skill.GetComponent<SkillProfile>().manaCost);
        //        spawning_skill = Instantiate(skill, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //        spawning_skill.SetActive(true);
        //        spawning_skill.transform.position = transform.position;
        //        spawning_skill.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
        //        spawning_skill.GetComponent<SkillProfile>().player_ID = playerControllerManager.playerID;
        //        spawning_skill.GetComponent<SkillProfile>().owner = gameObject;
        //        spawning_skill.GetComponent<SkillProfile>().findEnemy();

        //        spawning_skill.GetComponent<SkillProfile>().direction = gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection();
        //        spawning_skill.GetComponent<SkillProfile>().damagePerHit = spawning_skill.GetComponent<SkillProfile>().damagePerHit / 2;
        //    }
        //    else
        //    {
        //        wordingsHolder.showAndSetTiming(WORDING_TYPES.NOMANA, 1f);
        //        //Destroy(skill);
        //    }
        //}
	}

    public void resetTimer()
    {
        if (skill_cooldown == 0)
        {
            skill_cooldown = skill.GetComponent<SkillProfile>().castingCooldown;
        }
        timer = skill_cooldown;
    }
}
