using UnityEngine;
using System.Collections;

public class GiantStar : SkillProfile
{
    float checkEveryInterval_lifeTime = 0.0f;
    float damageTimer = 1f;

    bool runOnce = false;
    bool isComingBack = false;
    float rotatingDir = 1;
    // Update is called once per frame
    public override void Update () {
        checkEveryInterval_lifeTime += Time.deltaTime;
        damageTimer += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            direction = (owner.transform.position - enemy.transform.position).normalized;
            isComingBack = true;
            if (direction.x >= 0)
                rotatingDir = 1;
            else
                rotatingDir = -1;
            //gameObject.SetActive(false);
            //Destroy(gameObject);

            //send this object to despawn
            //checkEveryInterval_lifeTime = 0;
        }
        if (distToEnemy() < 1.0f && damageTimer >= 1f)
        {
            if (checkForCollision())
            {
                damageTimer = 0f;
                //gameObject.SetActive(false);
                //Destroy(gameObject);
                //send this object to despawn
            }
        }
        if (distToOwner() < 1.0f && isComingBack)
        {
            isComingBack = false;
            gameObject.SetActive(false);
            spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
            Destroy(gameObject);
        }

        if (!runOnce)
        {
            direction = (enemy.transform.position - owner.transform.position).normalized;
            runOnce = true;
            position = gameObject.transform.position;
            if (direction.x >= 0)
                rotatingDir = 1;
            else
                rotatingDir = -1;
        }

        position.x += direction.x * pSpeed * Time.deltaTime;
        gameObject.transform.position = position;
        gameObject.transform.Rotate(Vector3.forward* rotatingDir * Time.deltaTime * 200);
    }
    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
    }
}
