using UnityEngine;
using System.Collections;

public class BarrierSkill : SkillProfile
{
    float checkEveryInterval_lifeTime = 0.0f;
    // Use this for initialization
    public override void Start()
    {
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
        }
        

    }
}
