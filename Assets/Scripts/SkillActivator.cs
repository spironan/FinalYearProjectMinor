using UnityEngine;
using System.Collections;

public class SkillActivator : MonoBehaviour {

    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;

    public skillToActivate activator;

    public PlayerControllerManager playerControllerManager;

    public int player_number;

    private SkillProfile currentSkillProfile;
    private GameObject skill_gameObject;
    private int keyIterator;

    private bool dpadDown;

    // Use this for initialization
    void Start()
    {
        dpadDown = false;
        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        //Debug.Log(sprite_size);
        Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        //Debug.Log(local_sprite_size);
        if(player_number == 1)
        activator.gameObject.transform.position = new Vector2(0, transform.position.y + local_sprite_size.y + 0.1f);
        else
            activator.gameObject.transform.position = new Vector2(0, transform.position.y - local_sprite_size.y - 0.1f);
        //Debug.Log(Input.GetJoystickNames()[1]);  


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player_number);
        if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.X) )
        {
            Debug.Log(5);
            currentSkillProfile = skill1.GetComponent<SkillProfile>();
            createNewSkillObject();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill1 to activator
            //skill 1
            activator.generate_keys(currentSkillProfile.gameObject);
        }
        else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.Y))
        {
            currentSkillProfile = skill2.GetComponent<SkillProfile>();
            createNewSkillObject();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill2 to activator
            //skill 2
            activator.generate_keys(currentSkillProfile.gameObject);
        }
        else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.A))
        {

            currentSkillProfile = skill4.GetComponent<SkillProfile>();
            createNewSkillObject();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill4 to activator
            //skill 4
            activator.generate_keys(currentSkillProfile.gameObject);
        }
        else if (playerControllerManager.getIsKeyDown(BUTTON_INPUT.B))
        {
            
            currentSkillProfile = skill3.GetComponent<SkillProfile>();
            createNewSkillObject();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill3 to activator
            //skill 3
            activator.generate_keys(currentSkillProfile.gameObject);
        }


        

        if(currentSkillProfile != null)
        {
            
            //check for dpad presses
            //Debug.Log(Input.GetAxis("DPad_Y_xBox360"));
            if (keyIterator != currentSkillProfile.keysToActivate)
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
                }

                

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
                if (currentSkillProfile.keysToActivate == keyIterator && currentSkillProfile != null)
                {
                    currentSkillProfile.activateSkill = true;// activates the skills
                                                             // GameObject temp = Instantiate(currentSkillProfile.gameObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    skill_gameObject.SetActive(true);
                    skill_gameObject.transform.position = transform.position;
                    skill_gameObject.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<CharacterBase>().GetDirection(), 1);
                    skill_gameObject.GetComponent<SkillProfile>().player_ID = player_number;
                    skill_gameObject.GetComponent<SkillProfile>().owner = gameObject;
                    skill_gameObject.GetComponent<SkillProfile>().findEnemy();

                    skill_gameObject.GetComponent<SkillProfile>().direction = gameObject.GetComponent<CharacterBase>().GetDirection();
                    keyIterator = 0;
                    dpadDown = false;
                    currentSkillProfile = null;

                }
                activator.closeBorder();
            }
        }
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
        Destroy(skill_gameObject);
    }
}
