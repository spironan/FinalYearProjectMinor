﻿using UnityEngine;
using System.Collections;

public class BasicAttack : MonoBehaviour {
    public GameObject skill;
    public float skill_cooldown;

    float timer;
    GameObject spawning_skill;
    PlayerControllerManager playerControllerManager;
    // Use this for initialization
    void Start () {
        playerControllerManager = GetComponent<PlayerControllerManager>();
        timer = skill_cooldown;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.R1)
            && timer >= skill_cooldown)
        {
            timer = 0;
            spawning_skill = Instantiate(skill, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            spawning_skill.SetActive(true);
            spawning_skill.transform.position = transform.position;
            spawning_skill.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
            spawning_skill.GetComponent<SkillProfile>().player_ID = playerControllerManager.playerID;
            spawning_skill.GetComponent<SkillProfile>().owner = gameObject;
            spawning_skill.GetComponent<SkillProfile>().findEnemy();

            spawning_skill.GetComponent<SkillProfile>().direction = gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection();
        }
	}
}