using UnityEngine;
using System.Collections;

public class urgentChangeScene : MonoBehaviour 
{
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (gameManager.GetPlayerSize() > 0)
            LoadingScreenManager.LoadScene("CharacterSelectScene");
	}
}
