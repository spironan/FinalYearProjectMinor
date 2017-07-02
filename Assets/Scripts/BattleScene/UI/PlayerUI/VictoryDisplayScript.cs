using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryDisplayScript : MonoBehaviour
{
    //public TEAM playerTeam;
    Image[] victory;
    //PlayerInGameData playerInfo;
    int count;

    void Start()
    {
        //playerInfo = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().GetPlayer(playerTeam).GetInGameData();
        victory = GetComponentsInChildren<Image>();
    }

    public void WinMatch()
    {
        //victory[playerInfo.GetMatchWins() - 1].GetComponent<Image>().sprite = SpriteManager.GetInstance().GetSprite("VictorySprite");
        victory[count].GetComponent<Image>().sprite = SpriteManager.GetInstance().GetSprite("VictorySprite");
        count++;
    }

    public void ResetVictories()
    {
        foreach (Image image in victory)
            image.sprite = SpriteManager.GetInstance().GetSprite("Transparent");
        count = 0;
    }

}
