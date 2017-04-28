using UnityEngine;
using System.Collections;

public class skillToActivate : MonoBehaviour {

    public GameObject keyBox;
    public GameObject[] keys;
    //public GameObject skill;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void generate_keys(GameObject skill)
    {
        //first make the border active
        keyBox.SetActive(true);
        //set ze keyz by going thru a loop
    }
}
