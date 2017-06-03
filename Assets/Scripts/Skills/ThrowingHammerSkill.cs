using UnityEngine;
using System.Collections;

public class ThrowingHammerSkill : SkillProfile {

    public float speed_Y;
    float rotatingDir = 1;
    bool runOnce = false;

    public override void Start()
    {
        base.Start();
        speed_Y = pSpeed;
        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y, -1);
    }

    // Update is called once per frame
    public override void Update () {
        

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

        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * 200);
        if (distToEnemy() < 1.0f)
        {
            if (checkForCollision())
            {

                gameObject.SetActive(false);
                Destroy(gameObject);
                //send this object to despawn
            }
        }

        if( position.y < -10)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
        position.x += direction.x * pSpeed * Time.deltaTime;
        position.y += speed_Y * Time.deltaTime;
        speed_Y -= gravity * Time.deltaTime;
        gameObject.transform.position = position;
    }

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
                    enemy.GetComponent<PlayerCharacterLogicScript>().GainStunMeter(stunValuePerHit);
                    Debug.Log("hit");
                    //gameObject.SetActive(false);
                    return true;
                }
                else if(temp.collider.gameObject.tag == "Ground")
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override void reset()
    {
        runOnce = false;
        speed_Y = pSpeed;
    }
}
