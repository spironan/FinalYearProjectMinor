using UnityEngine;
using System.Collections;

public class FireBallStreamSkill : SkillProfile
{
    public GameObject fireBallskill;
    public float spawning_interval;
    GameObject spawning_skill;
    PlayerControllerManager playerControllerManager;

    float timer = 0;
    float timerForInterval = 0;
    public override void Start()
    {
        //base.Start();
        transform.parent = owner.transform;
        playerControllerManager = owner.GetComponent<PlayerCharacterLogicScript>().GetController();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        timerForInterval += Time.deltaTime;
        if (timer < lifetime)
        {
            if(timerForInterval > spawning_interval)
            {
                timerForInterval = 0;

                spawning_skill = Instantiate(fireBallskill, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                spawning_skill.SetActive(true);
                spawning_skill.transform.position = transform.position;
                spawning_skill.GetComponent<SkillProfile>().offSetSpawn(owner.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
                spawning_skill.GetComponent<SkillProfile>().player_ID = playerControllerManager.playerID;
                spawning_skill.GetComponent<SkillProfile>().owner = owner;
                spawning_skill.GetComponent<SkillProfile>().enemy = enemy;
            }
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
