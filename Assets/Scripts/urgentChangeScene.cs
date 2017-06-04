using UnityEngine;
using System.Collections;

public class urgentChangeScene : MonoBehaviour 
{
    public uint players;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (gameManager.GetPlayerSize() == players)
            LoadingScreenManager.LoadScene("CharacterSelectScene");
	}
}
