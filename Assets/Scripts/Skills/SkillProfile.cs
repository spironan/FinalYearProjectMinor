﻿using UnityEngine;
using System.Collections;

public enum PARTICLE_TYPE
{
    DISAPPEAR,
    MANA_GAIN,
    TOTAL,
};

public class SkillProfile : MonoBehaviour {
    public string nameOfSkill;
    public string descriptionOfSkill;
    [Range(1,5)]
    public int keysToActivate;
    [Range(0.0f, 100.0f)]
    public float stunValuePerHit;
    public PLAYER player_ID;
    [Range(0, 100)]
    public int damagePerHit;
    public float lifetime;
    public float pSpeed;
    public float gravity;
    public int manaCost;
    public int manaRegenPerHit;
    public float castingCooldown;
    public int UltGainPerHitForEnemy;

    public CircleCollider2D circleCollider;

    //[HideInInspector]
    public GameObject owner;
    public GameObject enemy;

    public Vector2 direction = new Vector2(0,0);
    


    [HideInInspector]
    public int[] directionToPress = new int[5];
    [HideInInspector]
    public float percentageOfManaCost = 0;
    public Sprite skillIcon;
    //[HideInInspector]
    public bool activateSkill = false;
    public GameObject[] particles;

    private PlayerCharacterLogicScript[] listOfPlayers;

    protected PlayerCharacterLogicScript enemyLogic;
    protected PlayerCharacterLogicScript ownerLogic;
    protected RaycastHit2D[] collision;
    protected Vector2 position;
    protected Vector2 sprite_size;
    protected Vector2 local_sprite_size;
    protected int damageMultipler = 1; 

    public virtual void determineKeyDirections()//virtual, so if dont want random spawn, we can set it ourselves
    {
        for(int i = 0; i < 5; ++i)
        {
            if(i < keysToActivate)
            {
                directionToPress[i] = Random.Range(0, 4);
               // Debug.Log(directionToPress[i]);
            }
            else
            {
                directionToPress[i] = -1;//set all the slots to -1
            }
            
        }
    }

    public virtual void Start()
    {
        sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
        local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        enemyLogic = enemy.GetComponent<PlayerCharacterLogicScript>();
        ownerLogic = owner.GetComponent<PlayerCharacterLogicScript>();

        UltGainPerHitForEnemy = 2 * damagePerHit;

        percentageOfManaCost = (float)manaCost / (float)ownerLogic.GetMaxManaCost();
    }

    public virtual float GetPercentageOfManaToMax()
    {
        if(percentageOfManaCost == 0)
        {
            percentageOfManaCost = (float)manaCost / (float)ownerLogic.GetMaxManaCost();

        }
        return percentageOfManaCost;
    }

    public virtual void Update()
    {

    }

    public virtual void findEnemy()
    {
        listOfPlayers = GameObject.FindObjectsOfType<PlayerCharacterLogicScript>();
        foreach (PlayerCharacterLogicScript cb in listOfPlayers)
        {
            if (cb.gameObject != owner)
            {
                enemy = cb.gameObject;
            }
        }
    }

    //public virtual void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("weee");
    //    if (collision.gameObject != owner && collision.gameObject.tag == "Player")
    //    {
    //        //do damage
    //        Debug.Log("hit");
    //    }
    //}
    public virtual bool checkForCollision()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),owner.GetComponent<Collider2D>());
        collision = Physics2D.CircleCastAll(transform.position, local_sprite_size.x/2, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject != owner)
                {
                    if (temp.collider.gameObject.GetComponent<PlayerCharacterLogicScript>() != null)
                    {
                        enemyLogic.GainStunMeter(stunValuePerHit);
                        enemyLogic.TakeDamage(damagePerHit * damageMultipler);
                        enemyLogic.GainUltMeter(UltGainPerHitForEnemy);
                        ownerLogic.increaseMana(manaRegenPerHit);
                        //Debug.Log("hit");
                    }
                    return true;
                }
            }
        }
        return false;
    }
    public virtual void offSetSpawn(Vector2 dir, float offset)
    {
        //Debug.Log("hi");
    }

    public virtual float distToEnemy()
    {
        return (enemy.transform.position - transform.position).magnitude;
    }
    public virtual float distToOwner()
    {
        return (owner.transform.position - transform.position).magnitude;
    }
    public virtual void reset()
    {

    }

    public virtual void setDirection(Vector2 dir)
    {
        direction = dir;
    }

    public virtual void spawnParticleEffect(PARTICLE_TYPE type, GameObject parentObj = null)
    {
        if(particles[(int)type] != null)
        {
            GameObject temp;
            temp = Instantiate(particles[(int)type], new Vector3(transform.position.x, transform.position.y, 2), Quaternion.Euler(0, 0, 0)) as GameObject;

            if (parentObj != null)
            {
                temp.transform.SetParent(parentObj.transform);
            }
        }
            
    }

    public virtual void spawnSkill(GameObject obj)
    {
        GameObject temp;
        temp = Instantiate(obj, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        temp.SetActive(true);
        temp.transform.position = transform.position;
        //temp.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
        temp.GetComponent<SkillProfile>().player_ID = player_ID;
        temp.GetComponent<SkillProfile>().owner = owner;
        temp.GetComponent<SkillProfile>().enemy = enemy;

        temp.GetComponent<SkillProfile>().direction = direction;
    }
}
