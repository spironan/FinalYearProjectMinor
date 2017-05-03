using UnityEngine;
using System.Collections;

public class FireSkill : SkillProfile {

    bool runOnce_spawnLoc = false;
	
	// Update is called once per frame
	void Update () {
        if(!runOnce_spawnLoc)
        {

        }
	    if(activateSkill)
        {
            //#need dir T_T
        }
	}
    public override void offSetSpawn(Vector2 dir, float offset)
    {
        //Debug.Log("bye");
        //the direction will be 0 when not moving :|
        //if (dir.x > 0)
        //    gameObject.transform.position = new Vector2(gameObject.transform.position.x + offset, gameObject.transform.position.y);
        //else if(dir.x < 0)
        //    gameObject.transform.position = new Vector2(gameObject.transform.position.x + offset, gameObject.transform.position.y);
    }
}
