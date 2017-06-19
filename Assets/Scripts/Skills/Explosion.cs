using UnityEngine;
using System.Collections;

public class Explosion : SkillProfile
{
    float checkEveryInterval_lifeTime = 0.0f;
    SkillActivator enemySkillActivator;
    // Use this for initialization
    public override void Start () {
        //base.Start();
        enemySkillActivator = enemy.GetComponent<SkillActivator>();
        //sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
        //local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        enemyLogic = enemy.GetComponent<PlayerCharacterLogicScript>();
        ownerLogic = owner.GetComponent<PlayerCharacterLogicScript>();
    }
	
	// Update is called once per frame
	public override void Update () {
        if(checkEveryInterval_lifeTime == 0)
        {
            checkForCollision();
        }
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            
            gameObject.SetActive(false);
            Destroy(gameObject);
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }
        //checkForCollision();
    }
    public override bool checkForCollision()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),owner.GetComponent<Collider2D>());
        collision = Physics2D.CircleCastAll(transform.position, 2f, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject != owner)
                {
                    if (temp.collider.gameObject.GetComponent<PlayerCharacterLogicScript>() != null)
                    {
                        enemy.GetComponent<PlayerCharacterLogicScript>().GainStunMeter(stunValuePerHit);
                        enemy.GetComponent<PlayerCharacterLogicScript>().TakeDamage(damagePerHit * damageMultipler);
                        enemy.GetComponent<PlayerCharacterLogicScript>().GainUltMeter(UltGainPerHitForEnemy);
                        Debug.Log("hit");
                        owner.GetComponent<PlayerCharacterLogicScript>().increaseMana(manaRegenPerHit);
                        enemySkillActivator.resetCurrentCastingSkill();
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
