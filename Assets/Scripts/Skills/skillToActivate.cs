using UnityEngine;
using System.Collections;

public class skillToActivate : MonoBehaviour {

    public GameObject keyBox;
    public GameObject[] keys;
    public Sprite[] PS4Keys;
    public Sprite[] XBOXKeys;

    public Sprite keybox_notReady;
    public Sprite keybox_Ready;

    private int numbersOfKeys;

    private SkillProfile currentSkillProfile;
    private string controllerName;
    private int numberOfCorrectKeys = -1;
    //public GameObject skill;
    // Use this for initialization
    void Start () {
        controllerName = transform.parent.GetComponent<PlayerCharacterLogicScript>().controller.nameOfController();
        //transform.position = Vector2.zero;
        //transform.position = new Vector3(0, transform.position.y,-1);
    }
	
	// Update is called once per frame
	void Update () {
        if(currentSkillProfile != null && keyBox.activeSelf)
        {
            if(numberOfCorrectKeys+1 == currentSkillProfile.keysToActivate
                && keyBox.GetComponent<SpriteRenderer>().sprite != keybox_Ready)
            {
                keyBox.GetComponent<SpriteRenderer>().sprite = keybox_Ready;
            }
        }
	}

    public void generate_keys(GameObject skill)
    {
        currentSkillProfile = skill.GetComponent<SkillProfile>();
        numbersOfKeys = currentSkillProfile.keysToActivate;
        currentSkillProfile.determineKeyDirections();
        numberOfCorrectKeys = -1;

        foreach (GameObject go in keys)
        {
            go.SetActive(false);// make sure all the keys is false
        }
        
        //first make the border active
        keyBox.SetActive(true);
        keyBox.GetComponent<SpriteRenderer>().sprite = keybox_notReady;
        //set ze keyz by going thru a loop

        for(int i = 0; i < numbersOfKeys; ++i)
        {
            //keys[i].transform.rotation = Quaternion.Euler(0,0,90 * currentSkillProfile.directionToPress[i]);
            
            
            keys[i].SetActive(true);
            keys[i].transform.GetChild(0).gameObject.SetActive(true);
            if(controllerName == "PS4")
                keys[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = PS4Keys[currentSkillProfile.directionToPress[i]];
            else
                keys[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = XBOXKeys[currentSkillProfile.directionToPress[i]];

            keys[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void showPressedCorrect(int keyToShowCorrect)
    {
        numberOfCorrectKeys = keyToShowCorrect;
        keys[keyToShowCorrect].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void closeBorder()
    {
        currentSkillProfile = null;
        keyBox.SetActive(false);

    }
}
