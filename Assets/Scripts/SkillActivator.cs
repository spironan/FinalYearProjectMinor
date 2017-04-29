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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("X_button_xBox360") && player_number == 1
            || Input.GetButtonDown("X_button_xBox360") && player_number == 2)
        {
            currentSkillProfile = skill1.GetComponent<SkillProfile>();
            keyIterator = 0;
            //make sure the skill knows the owner first
            currentSkillProfile.player_ID = player_number;
            //pass skill1 to activator
            //skill 1
            activator.generate_keys(skill1);
        }
        else if (Input.GetButtonDown("Y_button_xBox360"))
        {
            //pass skill1 to activator
            //skill 2
        }
        else if (Input.GetButtonDown("A_button_xBox360"))
        {

            //pass skill1 to activator
            //skill 3
        }
        else if (Input.GetButtonDown("B_button_xBox360"))
        {
            //pass skill1 to activator
            //skill 4
        }

        if(currentSkillProfile != null)
        {
            //check for dpad presses
            //Debug.Log(Input.GetAxis("DPad_Y_xBox360"));
            if (Input.GetAxis("DPad_Y_xBox360") > 0)//up?
            {
                //Debug.Log(5);
                dpadDown = true;
            }
            else if (Input.GetAxis("DPad_Y_xBox360") < 0)//down?
            {
                dpadDown = true;
            }
            else if (Input.GetAxis("DPad_X_xBox360") > 0)//right
            {
                dpadDown = true;
            }
            else if (Input.GetAxis("DPad_Y_xBox360") < 0)//left
            {
                dpadDown = true;
            }
            else
            {
                dpadDown = false;
            }

        }
    }
}
