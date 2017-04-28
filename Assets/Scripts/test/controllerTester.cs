using UnityEngine;
using System.Collections;

public class controllerTester : MonoBehaviour {


    public GameObject test_A;
    public GameObject test_B;
    public GameObject test_X;
    public GameObject test_Y;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButton("A_button_player1"))
        {
            test_A.SetActive(false);
        }
        else
        {
            test_A.SetActive(true);
        }
	}
}
