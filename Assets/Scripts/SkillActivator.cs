using UnityEngine;
using System.Collections;

public class SkillActivator : MonoBehaviour {

    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;

    public skillToActivate activator;

    public int player_number;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("X_button_xBox360") && player_number == 1
            || Input.GetButtonDown("X_button_xBox360") && player_number == 2)
        {
            //pass skill1 to activator
            activator.generate_keys(skill1);
        }
        if (Input.GetButtonDown("Y_button_xBox360"))
        {

        }
        if (Input.GetButtonDown("A_button_xBox360"))
        {

        }
        if (Input.GetButtonDown("B_button_xBox360"))
        {

        }
    }
}
