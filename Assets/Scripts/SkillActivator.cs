using UnityEngine;
using System.Collections;

public class SkillActivator : MonoBehaviour {

    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;

    public skillToActivate activator;

    public int player_number;

    private SkillProfile currentSkillProfile;
    private int keyIterator;

    private bool dpadDown;

    // Use this for initialization
    void Start()
    {
        dpadDown = false;
        Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
        Debug.Log(sprite_size);
        Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Debug.Log(local_sprite_size);
        activator.gameObject.transform.position = new Vector2(0, transform.position.y + local_sprite_size.y + 0.1f);
        //Debug.Log(Input.GetJoystickNames()[1]);  
        
          
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("X_button_xBox360") && player_number == 1)
            || (Input.GetButtonDown("X_button_xBox360_player2") && player_number == 2))
        {
            Debug.Log(5);
            currentSkillProfile = skill1.GetComponent<SkillProfile>();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill1 to activator
            //skill 1
            activator.generate_keys(skill1);
        }
        else if (Input.GetButtonDown("Y_button_xBox360") && player_number == 1
            || Input.GetButtonDown("Y_button_xBox360_player2") && player_number == 2)
        {
            currentSkillProfile = skill2.GetComponent<SkillProfile>();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill2 to activator
            //skill 2
            activator.generate_keys(skill2);
        }
        else if (Input.GetButtonDown("A_button_xBox360") && player_number == 1
            || Input.GetButtonDown("A_button_xBox360_player2") && player_number == 2)
        {

            currentSkillProfile = skill4.GetComponent<SkillProfile>();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill4 to activator
            //skill 4
            activator.generate_keys(skill4);
        }
        else if (Input.GetButtonDown("B_button_xBox360") && player_number == 1
            || Input.GetButtonDown("B_button_xBox360_player2") && player_number == 2)
        {
            
            currentSkillProfile = skill3.GetComponent<SkillProfile>();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill3 to activator
            //skill 3
            activator.generate_keys(skill3);
        }


        

        if(currentSkillProfile != null)
        {
            
            //check for dpad presses
            //Debug.Log(Input.GetAxis("DPad_Y_xBox360"));
            if (keyIterator != currentSkillProfile.keysToActivate)
            {
                int keyValue = -1;
                if (Input.GetAxis("DPad_Y_xBox360") > 0 && player_number == 1
                    || Input.GetAxis("DPad_Y_xBox360_player2") > 0 && player_number == 2)//up?
                {
                    keyValue = 0;
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
                        keyIterator = 0;
                    }

                }
                else if (Input.GetAxis("DPad_Y_xBox360") < 0 && player_number == 1
                    || Input.GetAxis("DPad_Y_xBox360_player2") < 0 && player_number == 2)//down?
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
                        keyIterator = 0;
                    }
                }
                else if (Input.GetAxis("DPad_X_xBox360") > 0 && player_number == 1
                    || Input.GetAxis("DPad_X_xBox360_player2") > 0 && player_number == 2)//right
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
                        keyIterator = 0;
                    }
                }
                else if (Input.GetAxis("DPad_X_xBox360") < 0 && player_number == 1
                    || Input.GetAxis("DPad_X_xBox360_player2") < 0 && player_number == 2)//left
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

            if (Input.GetButtonDown("L1_button_xBox360") && player_number == 1
                    || Input.GetButtonDown("L1_button_xBox360_player2") && player_number == 2)
            {
                if (currentSkillProfile.keysToActivate == keyIterator && currentSkillProfile != null)
                {
                    currentSkillProfile.activateSkill = true;// activates the skills
                    GameObject temp = Instantiate(currentSkillProfile.gameObject, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                    temp.GetComponent<SkillProfile>().offSetSpawn(gameObject.GetComponent<CharacterBase>().GetDirection(), 1);
                    temp.GetComponent<SkillProfile>().player_ID = player_number;
                    temp.GetComponent<SkillProfile>().owner = gameObject;
                    temp.GetComponent<SkillProfile>().findEnemy();

                    temp.GetComponent<SkillProfile>().direction = gameObject.GetComponent<CharacterBase>().GetDirection();
                    keyIterator = 0;
                    dpadDown = false;
                    currentSkillProfile = null;

                }
                activator.closeBorder();
            }
        }
    }


}
