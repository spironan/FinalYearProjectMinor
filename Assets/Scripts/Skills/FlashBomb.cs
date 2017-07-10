using UnityEngine;
using System.Collections;
public class FlashBomb : SkillProfile
{
    protected float checkEveryInterval_lifeTime = 0.0f;
    protected bool runOnce = false;
    protected float rotatingDir = 1;
    protected bool isSetDirection = false;
    public override void Update()
    {
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
            gameObject.SetActive(false);
            Destroy(gameObject);

            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }

        //if (distToEnemy() < 1.0f)
        {
            if (checkForCollision())
            {
                spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
                gameObject.SetActive(false);
                Destroy(gameObject);
                //send this object to despawn
            }
        }
        if (!runOnce)
        {
            if (!isSetDirection)
                direction = (enemy.transform.position - owner.transform.position).normalized;
            runOnce = true;
            isSetDirection = true;
            position = gameObject.transform.position;
            if (direction.x >= 0)
                rotatingDir = 1;
            else
                rotatingDir = -1;
        }

        position.x += direction.x * pSpeed * Time.deltaTime;
        //position.y += direction.y * pSpeed * Time.deltaTime;
        gameObject.transform.position = position;
        //base.Update();
    }

    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
        enemyLogic = enemy.GetComponent<PlayerCharacterLogicScript>();
        ownerLogic = owner.GetComponent<PlayerCharacterLogicScript>();
        if (isSetDirection)
        {
            direction = -direction;
        }
    }

    public override void setDirection(Vector2 dir)
    {
        direction = dir;
        isSetDirection = true;
    }
}
