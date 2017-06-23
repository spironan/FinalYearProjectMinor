using UnityEngine;
using System.Collections;

public class FireSkill : SkillProfile {
    
    protected float checkEveryInterval_lifeTime = 0.0f;

    protected bool runOnce = false;
    protected bool isSetDirection = false;
    protected float rotatingDir = 1;


    // Update is called once per frame
    public override void Update () {
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }
        if(distToEnemy() < 1.0f)
        {
            if(checkForCollision())
            {
                spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
                gameObject.SetActive(false);
                Destroy(gameObject);
                //send this object to despawn
            }
        }

        
        if (!runOnce)
        {
            if(!isSetDirection)
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
        position.y += direction.y * pSpeed * Time.deltaTime;
        gameObject.transform.position = position;
        
	}
    //public override void offSetSpawn(Vector2 dir, float offset)
    //{
        
    //}

    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
        if(isSetDirection)
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
