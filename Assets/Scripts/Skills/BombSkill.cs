using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BombSkill : SkillProfile {
    public GameObject explosion;

    float checkEveryInterval_lifeTime = 0.0f;

    bool runOnce = false;
    bool doDamage = true;
    bool enableCollisionToOwner = false;


    //private CircleCollider2D circleColliderSelf;
    private Rigidbody2D rigidBody;

    public override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        //circleColliderSelf = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    public override void Update()
    {
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }
        if (checkEveryInterval_lifeTime > lifetime - 0.2f && doDamage)
        {
            checkForCollision();
            doDamage = false;
        }


        if (!runOnce)
        {
            direction = (enemy.transform.position - owner.transform.position).normalized;
            runOnce = true;
            position = gameObject.transform.position;
            rigidBody.velocity = new Vector2(direction.x * 5, 5);


        }
        if(!enableCollisionToOwner)
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
    public override void offSetSpawn(Vector2 dir, float offset)
    {

    }
    public override bool checkForCollision()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),owner.GetComponent<Collider2D>());
        collision = Physics2D.CircleCastAll(transform.position, local_sprite_size.x*3, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "Player" && temp.collider.gameObject != owner)
                {
                    if (temp.collider.gameObject.GetComponent<StunMeterManager>() != null)
                    {
                        temp.collider.gameObject.GetComponent<StunMeterManager>().addStunValue(stunValuePerHit);
                    }
                    Debug.Log("hit");
                    //gameObject.SetActive(false);
                    return true;
                }
            }
        }
        return false;
    }
    public override void reset()
    {
        runOnce = false;
        checkEveryInterval_lifeTime = 0;
    }

    public float distToOwner()
    {
        return (transform.position - owner.transform.position).magnitude;
    }
}
