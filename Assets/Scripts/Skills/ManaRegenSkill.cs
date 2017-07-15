using UnityEngine;
using System.Collections;

public class ManaRegenSkill : SkillProfile {

    [Range(0,100)]
    public int manaRegenValue;
    // Update is called once per frame
    public override void Start()
    {
        //base.Start();
    }
    public override void Update () {
        owner.GetComponent<PlayerCharacterLogicScript>().increaseMana(manaRegenValue);
        spawnParticleEffect(PARTICLE_TYPE.MANA_GAIN,owner);
        Destroy(gameObject);
    }
}
