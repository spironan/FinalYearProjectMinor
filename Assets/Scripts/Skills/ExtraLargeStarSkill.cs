using UnityEngine;
using System.Collections;

public class ExtraLargeStarSkill : SkillProfile
{
    public int laps;

    float checkEveryInterval_lifeTime = 0.0f;
    float damageTimer = 1f;

    bool runOnce = false;
    //bool isComingBack = false;
    int currentLaps = 0;
    float rotatingDir = 1;
    float timeForEachLap;
    //float timeBeforeTurning = 0f;
    public override void Start()
    {
        base.Start();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        timeForEachLap = lifetime / laps;
    }
    // Update is called once per frame
    public override void Update()
    {
        checkEveryInterval_lifeTime += Time.deltaTime;
        damageTimer += Time.deltaTime;

        if (checkEveryInterval_lifeTime > timeForEachLap)
        {
            //if (!isComingBack)
            //{
            //direction = (owner.transform.position - enemy.transform.position).normalized;
            direction = -direction;
            //isComingBack = true;
                if (direction.x >= 0)
                    rotatingDir = 1;
                else
                    rotatingDir = -1;
            //}
            //else
            //{
            //    isComingBack = false;
            //    runOnce = false;
            //}
            checkEveryInterval_lifeTime = 0f;
            currentLaps += 1;
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
        if (distToEnemy() < 1.0f && damageTimer >= 1f)
        {
            if (checkForCollision())
            {
                damageTimer = 0f;
            }
        }
        if (currentLaps > laps)
        {
            //isComingBack = false;
            spawnParticleEffect(PARTICLE_TYPE.DISAPPEAR);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        

        position += direction * pSpeed * Time.deltaTime;
        gameObject.transform.position = position;
        gameObject.transform.Rotate(Vector3.forward * rotatingDir * Time.deltaTime * 200);
    }
    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
    }
}
