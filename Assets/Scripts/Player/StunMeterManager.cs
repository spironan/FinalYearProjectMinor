using UnityEngine;
using System.Collections;

public class StunMeterManager : MonoBehaviour {

    public StunMeter stunMeter;
    public WordingsHolder StunnedTitle;
    //public bool isStunned;

    private PLAYER player_number;
    private PlayerControllerManager playerControllerManager;
    public float stunTime = 0.0f;

    private SkillActivator activator;
    // Use this for initialization
    void Start()
    {
        playerControllerManager = GetComponent<PlayerCharacterLogicScript>().GetController();
        //stunMeter = GetComponentInChildren<StunMeter>();
        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;

        Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        player_number = GetComponent<PlayerCharacterLogicScript>().GetPlayerID();
        if (player_number == PLAYER.PLAYER_ONE)
            stunMeter.gameObject.transform.position += new Vector3(0, -local_sprite_size.y - 0.1f, stunMeter.gameObject.transform.position.z);
        else if (player_number == PLAYER.PLAYER_TWO)
            stunMeter.gameObject.transform.position += new Vector3(0, local_sprite_size.y + 0.1f, stunMeter.gameObject.transform.position.z);
        activator = GetComponent<SkillActivator>();

        StunnedTitle = GetComponentInChildren<WordingsHolder>();
        //.changeWording(WORDING_TYPES.STUNNED,player_number);
    }
	
	// Update is called once per frame
	void Update () {
	    if(stunMeter.getStunValue() < 0.5f && stunMeter.gameObject.activeSelf)
        {
            stunMeter.gameObject.SetActive(false);
        }
        else if(stunMeter.getStunValue() > 0.5f && !stunMeter.gameObject.activeSelf)
        {
            stunMeter.gameObject.SetActive(true);
        }
        if(playerControllerManager.isControllerDisabled())
        {
            stunTime += Time.deltaTime;
        }
        //else
        //{
        //    stunTime = 0;
        //}
        if(stunMeter.getStunValue() == 100 && !playerControllerManager.isControllerDisabled())
        {
            playerControllerManager.DisableController();
            activator.resetCurrentCastingSkill();
            StunnedTitle.showAndSetTiming(WORDING_TYPES.STUNNED,3f);
        }
        else if(stunTime >= 3.0f && playerControllerManager.isControllerDisabled())
        {
            //StunnedTitle.hideWording();
            playerControllerManager.EnableController();
            stunTime = 0;
        }
        //if (Input.GetKey(KeyCode.Z))
        //{
        //    addStunValue(10);
        //}
    }

    public void addStunValue(float value)
    {
        if(!playerControllerManager.isControllerDisabled())
            stunMeter.addStunValue(value);
    }

    public void resetStunValue()
    {
        stunMeter.setStunValue(0f);
    }

    public void pauseStunDecrease()
    {
        stunMeter.setToStopStunDecrease(true);
    }

    public void resumeStunDecrease()
    {
        stunMeter.setToStopStunDecrease(false);
    }
}
