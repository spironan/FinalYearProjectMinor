using UnityEngine;
using System.Collections;

public class skillToActivate : MonoBehaviour {

    public GameObject keyBox;
    public GameObject[] keys;

    private int numbersOfKeys;

    private SkillProfile currentSkillProfile;
    //public GameObject skill;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void generate_keys(GameObject skill)
    {
        currentSkillProfile = skill.GetComponent<SkillProfile>();
        numbersOfKeys = currentSkillProfile.keysToActivate;
        currentSkillProfile.determineKeyDirections();


        foreach (GameObject go in keys)
        {
            go.SetActive(false);// make sure all the keys is false
        }
        
        //first make the border active
        keyBox.SetActive(true);
        //set ze keyz by going thru a loop

        for(int i = 0; i < numbersOfKeys; ++i)
        {
            keys[i].transform.rotation = Quaternion.Euler(0,0,90 * currentSkillProfile.directionToPress[i]);
            
            
            keys[i].SetActive(true);
            keys[i].transform.GetChild(0).gameObject.SetActive(true);
            keys[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
