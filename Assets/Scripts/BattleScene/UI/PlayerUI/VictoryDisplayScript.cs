using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryDisplayScript : MonoBehaviour
{
    //public TEAM playerTeam;
    Image[] victory;
    int count;
    Sprite transparent, victorySprite;

    void Start()
    {
        //playerInfo = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetPlayer(playerTeam).GetInGameData();
        victory = GetComponentsInChildren<Image>();
        transparent = SpriteManager.GetInstance().GetSprite("Transparent");
        victorySprite = SpriteManager.GetInstance().GetSprite("VictorySprite");
    }

    public void WinMatch()
    {
        //victory[playerInfo.GetMatchWins() - 1].GetComponent<Image>().sprite = SpriteManager.GetInstance().GetSprite("VictorySprite");
        victory[count].GetComponent<Image>().sprite = victorySprite;
        count++;
    }

    public void ResetVictories()
    {
        foreach (Image image in victory)
            image.sprite = transparent;
        count = 0;
    }

}
