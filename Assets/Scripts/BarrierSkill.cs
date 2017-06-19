using UnityEngine;
using System.Collections;

public class BarrierSkill : SkillProfile
{
    [Range(0,100)]
    public int Defence;
    [Range(0, 100)]
    public float StunResistance;
    float checkEveryInterval_lifeTime = 0.0f;
    //PlayerCharacterLogicScript ownerLogic;
    //bool runOnce = false;


    // Use this for initialization
    public override void Start()
    {
        //sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
        //local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        ownerLogic = owner.GetComponent<PlayerCharacterLogicScript>();
        //enemyLogic = enemy.GetComponent<PlayerCharacterLogicScript>();
        ownerLogic.setGeneralResistance(Defence);
        ownerLogic.setStunResistance(StunResistance);
    }

    // Update is called once per frame
    public override void Update()
    {
        //gameObject.transform.position = Vector2.zero;
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
            ownerLogic.setGeneralResistance(0);
            ownerLogic.setStunResistance(0);
        }

        

    }
}
