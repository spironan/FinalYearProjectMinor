using UnityEngine;
using System.Collections;

public class NinjaStarSkill : FireSkill
{
    SkillActivator enemySkillActivator;
    public override void Start()
    {
        base.Start();
        enemySkillActivator = enemy.GetComponent<SkillActivator>();

    }
    //thinking for ways to make this unique hurrr
    public override bool checkForCollision()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),owner.GetComponent<Collider2D>());
        collision = Physics2D.CircleCastAll(transform.position, local_sprite_size.x / 2, Vector2.zero, 0);

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
                        Debug.Log("hit");
                        ownerLogic.increaseMana(manaRegenPerHit);
                        enemySkillActivator.resetCurrentCastingSkill();
                        //gameObject.SetActive(false);
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
