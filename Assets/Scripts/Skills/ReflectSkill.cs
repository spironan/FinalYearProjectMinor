using UnityEngine;
using System.Collections;

public class ReflectSkill : SkillProfile
{
    float checkEveryInterval_lifeTime = 0.0f;
    // Use this for initialization
    public override void Start()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
        base.Start();
        //transform.position = new Vector2(0, 0);
        transform.SetParent(owner.transform);
        transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y, -1);
    }

    // Update is called once per frame
    public override void Update () {
        //gameObject.transform.position = Vector2.zero;
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }
        checkForCollision();

	}


    public override bool checkForCollision()
    {
        collision = Physics2D.CircleCastAll(transform.position, local_sprite_size.x / 2, Vector2.zero, 0);

        foreach (RaycastHit2D temp in collision)
        {
            if (temp.collider != null)
            {
                if (temp.collider.gameObject.tag == "RangedSkill" && temp.collider.gameObject.GetComponent<SkillProfile>().owner != owner)
                {
                    Debug.Log("reflect");
                    temp.collider.gameObject.GetComponent<SkillProfile>().reset();
                    temp.collider.gameObject.GetComponent<SkillProfile>().enemy = temp.collider.gameObject.GetComponent<SkillProfile>().owner;
                    temp.collider.gameObject.GetComponent<SkillProfile>().owner = owner;
                    //gameObject.SetActive(false);
                    return true;
                }
            }
        }
        return false;
    }
}
