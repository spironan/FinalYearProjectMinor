using UnityEngine;
using System.Collections;

public class stunMeter : MonoBehaviour {

    [Range(0.01f,100.0f)]
    public float stunValue;
    public GameObject border;
    public GameObject fill;

    private float maxStunValue = 100.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
