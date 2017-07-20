using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTextTips : MonoBehaviour {

    public List<string> tipsMessages;
    Text tip;

	// Use this for initialization
	void Start () {
        tip = GetComponent<Text>();
        tip.text = tipsMessages[Random.Range(0, tipsMessages.Count)];
    }
	
}
