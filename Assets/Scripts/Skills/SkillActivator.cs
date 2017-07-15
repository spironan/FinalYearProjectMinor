using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PlayerControllerManager))]
//[RequireComponent(typeof(ListOfControllerActions))]
//[RequireComponent(typeof(PlayerCharacterLogicScript))]
public class SkillActivator : MonoBehaviour {

    public GameObject RegenSkill;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;
    public GameObject skillULTIMATE;

    public skillToActivate activator;

    public PlayerControllerManager playerControllerManager;
    public ListOfControllerActions bindedActions;

    //public int player_number;
    public PLAYER player_number;

    private SkillProfile currentSkillProfile;
    private GameObject skill_gameObject;
    private int keyIterator;

    private bool dpadDown;
    private bool selectedSkill = false;
    private bool isCastingUlti = false;
    private bool failedToCast = false;

    private PlayerCharacterLogicScript owner;
    private WordingsHolder wordingsHolder;
    public bool GetIsCastingUlti() { return isCastingUlti; }

    private int keyValue = -1;
    // Use this for initialization
    void Start()
    {
        dpadDown = false;
        //activator = GetComponentInChildren<skillToActivate>();
        owner = GetComponent<PlayerCharacterLogicScript>();
        playerControllerManager = owner.GetController();
        player_number = owner.GetPlayerID();
        //playerControllerManager = GetComponent<PlayerControllerManager>();
        //playerControllerManager.init(player_number);
        //bindedActions = GetComponent<ListOfControllerActions>();

        activator = transform.GetChild(0).GetComponent<skillToActivate>();
        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;

        Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        if(player_number == PLAYER.PLAYER_ONE)
            activator.gameObject.transform.position += new Vector3(0, local_sprite_size.y + 0.1f, activator.gameObject.transform.position.z);
        else if(player_number == PLAYER.PLAYER_TWO)
            activator.gameObject.transform.position += new Vector3(0, -local_sprite_size.y - 0.1f, activator.gameObject.transform.position.z);

        wordingsHolder = GetComponent<WordingsHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player_number != owner.GetPlayerID())
        //    player_number = owner.GetPlayerID();
        //if(playerControllerManager == null)
        //    playerControllerManager = GetComponent<PlayerControllerManager>();

        //Debug.Log(player_number);


        failedToCast = false;

        if (currentSkillProfile != null)
        {

            //check for dpad presses
            //Debug.Log(Input.GetAxis("DPad_Y_xBox360"));
            
            if (keyIterator != currentSkillProfile.keysToActivate && selectedSkill)
            {
                keyValue = -1;
                
                if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.Y))//up?
                {
                    
                    keyValue = 0;
                    
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        //Debug.Log(player_number);
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                        failedToCast = true;
                    }

                }
                else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.A))//down?
                {
                    keyValue = 2;
                    
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                        failedToCast = true;
                    }
                }
                else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.B))//right
                {
                    keyValue = 3;
                    
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                        failedToCast = true;
                    }
                }
                else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.X))//left
                {
                    keyValue = 1;
                    
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                        failedToCast = true;
                        //dpadDown = false;
                    }
                }
                else
                {
                    dpadDown = false;
                }

                //keyValue = -1;

            }
            
            //else if(keyValue != currentSkillProfile.directionToPress[keyIterator]
            //    && dpadDown)
            //{
            //    activator.closeBorder();
            //    keyIterator = 0;
            //}
            //Debug.Log(Input.GetAxis("DPad_Y_xBox360"));

            if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.L1))
            {
                
                if (currentSkillProfile.keysToActivate == keyIterator && currentSkillProfile != null
                    && owner.getManaAmount() >= skill_gameObject.GetComponent<SkillProfile>().manaCost)
                {
                    if (!isCastingUlti)
                        owner.decreaseMana(skill_gameObject.GetComponent<SkillProfile>().manaCost);
                    else
                        owner.SetUltiChargeAmount(0);
                    currentSkillProfile.activateSkill = true;// activates the skills
                                                             // GameObject temp = Instantiate(currentSkillProfile.gameObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    skill_gameObject.SetActive(true);
                    skill_gameObject.transform.position = transform.position;
                    //skill_gameObject.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection(), 1);
                    skill_gameObject.GetComponent<SkillProfile>().player_ID = player_number;
                    skill_gameObject.GetComponent<SkillProfile>().owner = gameObject;
                    skill_gameObject.GetComponent<SkillProfile>().findEnemy();

                    skill_gameObject.GetComponent<SkillProfile>().direction = gameObject.GetComponent<PlayerCharacterLogicScript>().GetDirection();
                    keyIterator = 0;
                    dpadDown = false;
                    currentSkillProfile = null;

                }
                else if(owner.getManaAmount() < skill_gameObject.GetComponent<SkillProfile>().manaCost)
                {
                    wordingsHolder.showAndSetTiming(WORDING_TYPES.NOMANA, 1f);
                }
                selectedSkill = false;
                isCastingUlti = false;
                activator.closeBorder();
            }
        }
        
        checkSkillToActivate();
        
    }


    void createNewSkillObject()
    {
        if(skill_gameObject != null)
        {
            if (skill_gameObject.activeSelf)
                skill_gameObject = null;
            else
                Destroy(skill_gameObject);
        }
        skill_gameObject = Instantiate(currentSkillProfile.gameObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentSkillProfile = skill_gameObject.GetComponent<SkillProfile>();
    }

    void destroyInactiveSkill()
    {
        selectedSkill = false;
        isCastingUlti = false;
        Destroy(skill_gameObject);
    }

    void checkSkillToActivate()
    {

        if (!selectedSkill && !failedToCast)
        {
            if (bindedActions.getButtonAction(ACTIONS.SKILL_ONE)
                && haveEnoughManaForSkill(skill1.GetComponent<SkillProfile>()))
            {
                Debug.Log(5);
                currentSkillProfile = skill1.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                //pass skill1 to activator
                //skill 1
                activator.generate_keys(currentSkillProfile.gameObject);
            }
            else if (bindedActions.getButtonAction(ACTIONS.SKILL_TWO)
                && haveEnoughManaForSkill(skill2.GetComponent<SkillProfile>()))
            {
                currentSkillProfile = skill2.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                //pass skill2 to activator
                //skill 2
                activator.generate_keys(currentSkillProfile.gameObject);
            }
            else if (bindedActions.getButtonAction(ACTIONS.SKILL_FOUR)
                && haveEnoughManaForSkill(skill4.GetComponent<SkillProfile>()))
            {

                currentSkillProfile = skill4.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                //pass skill4 to activator
                //skill 4
                activator.generate_keys(currentSkillProfile.gameObject);
            }
            else if (bindedActions.getButtonAction(ACTIONS.SKILL_THREE)
                && haveEnoughManaForSkill(skill3.GetComponent<SkillProfile>()))
            {

                currentSkillProfile = skill3.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                //pass skill3 to activator
                //skill 3
                activator.generate_keys(currentSkillProfile.gameObject);
            }
            else if (bindedActions.getButtonAction(ACTIONS.SKILL_REGEN)
                && haveEnoughManaForSkill(RegenSkill.GetComponent<SkillProfile>()))
            {

                currentSkillProfile = RegenSkill.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                activator.generate_keys(currentSkillProfile.gameObject);
            }
            else if (bindedActions.getButtonAction(ACTIONS.SKILL_ULTIMATE) && owner.GetUltiPercentage() >= 1f)
            {
                currentSkillProfile = skillULTIMATE.GetComponent<SkillProfile>();
                createNewSkillObject();
                keyIterator = 0;
                selectedSkill = true;
                isCastingUlti = true;
                //pass ulti to activator
                //skill ulti
                activator.generate_keys(currentSkillProfile.gameObject);
            }
        }
    }

    bool haveEnoughManaForSkill(SkillProfile skill)
    {
        if(owner.getManaAmount() >= skill.GetComponent<SkillProfile>().manaCost)
        {
            return true;
        }
        else
        {
            wordingsHolder.showAndSetTiming(WORDING_TYPES.NOMANA, 1f);
            return false;
        }
    }

    public void resetCurrentCastingSkill()
    {
        isCastingUlti = false;
        activator.closeBorder();
        destroyInactiveSkill();
        keyIterator = 0;
    }
}


/*if (keyIterator != currentSkillProfile.keysToActivate)
            {
                int keyValue = -1;
                
                if (playerControllerManager.getValueFromAxis(JOYSTICK_AXIS_INPUT.DPAD_UP).getBool())//up?
                {
                    
                    keyValue = 0;
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        //Debug.Log(player_number);
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                    }

                }
                else if (playerControllerManager.getValueFromAxis(JOYSTICK_AXIS_INPUT.DPAD_DOWN).getBool())//down?
                {
                    keyValue = 2;
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                    }
                }
                else if (playerControllerManager.getValueFromAxis(JOYSTICK_AXIS_INPUT.DPAD_RIGHT).getBool())//right
                {
                    keyValue = 3;
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                    }
                }
                else if (playerControllerManager.getValueFromAxis(JOYSTICK_AXIS_INPUT.DPAD_LEFT).getBool())//left
                {
                    keyValue = 1;
                    if (keyValue == currentSkillProfile.directionToPress[keyIterator]
                     && !dpadDown)//doing this so it wont keep taking same input IMSORRY IF THIS IS HARDCODE T^T
                    {
                        activator.showPressedCorrect(keyIterator);
                        keyIterator += 1;
                        dpadDown = true;
                    }
                    else if (keyValue != currentSkillProfile.directionToPress[keyIterator]
                    && !dpadDown)
                    {
                        activator.closeBorder();
                        destroyInactiveSkill();
                        keyIterator = 0;
                        //dpadDown = false;
                    }
                }
                else
                {
                    dpadDown = false;
                }*/
