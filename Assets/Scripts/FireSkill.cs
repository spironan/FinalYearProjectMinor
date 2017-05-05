using UnityEngine;
using System.Collections;

public class FireSkill : SkillProfile {
    
    float checkEveryInterval_lifeTime = 0.0f;
    
    bool runOnce = false;

    

    // Update is called once per frame
    public override void Update () {
        checkEveryInterval_lifeTime += Time.deltaTime;
        if (checkEveryInterval_lifeTime > lifetime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            //send this object to despawn
            checkEveryInterval_lifeTime = 0;
        }
        if(distToEnemy() < 1.0f)
        {
            if(checkForCollision())
            {
                
                gameObject.SetActive(false);
                Destroy(gameObject);
                //send this object to despawn
            }
        }

        
        if (!runOnce)
        {
            direction = (enemy.transform.position - owner.transform.position).normalized;
            runOnce = true;
            position = gameObject.transform.position;
        }
	    
        position.x += direction.x * pSpeed * Time.deltaTime;
        gameObject.transform.position = position;
        
	}
    public override void offSetSpawn(Vector2 dir, float offset)
    {
        
    }
}
