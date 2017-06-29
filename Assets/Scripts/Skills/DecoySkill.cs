using UnityEngine;
using System.Collections;

public class DecoySkill : SkillProfile {

    public GameObject explosion;

    float checkEveryInterval_lifeTime = 0.0f;
    float throwingDir = 1;

    bool runOnce = false;
    //bool doDamage = true;
    bool enableCollisionToOwner = false;

    Rigidbody2D rigidBody;


    public override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
    }
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
        //if (checkEveryInterval_lifeTime > lifetime - 0.2f && doDamage)
        //{
        //    //checkForCollision();
        //    //do poof effect
        //    doDamage = false;
        //}


        if (!runOnce)
        {
            direction = (enemy.transform.position - owner.transform.position).normalized;
            runOnce = true;
            position = gameObject.transform.position;

            if (direction.x > 0)
                throwingDir = 1;
            else
                throwingDir = -1;
            rigidBody.velocity = new Vector2(throwingDir * 1.5f, 5);


        }
        if (!enableCollisionToOwner)
        {
            if (distToOwner() > (local_sprite_size.x / 2))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>(), false);
            }
        }
        //
        //position.x += direction.x * pSpeed * Time.deltaTime;
        //gameObject.transform.position = position;

    }

    
    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
    }
}
