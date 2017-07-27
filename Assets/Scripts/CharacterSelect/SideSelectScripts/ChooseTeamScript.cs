using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseTeamScript : MonoBehaviour {

    public TEAM team;
    TEAM opposingTeam;

    void Start()
    {
        if (team == TEAM.RED_TEAM)
            opposingTeam = TEAM.BLUE_TEAM;
        else if (team == TEAM.BLUE_TEAM)
            opposingTeam = TEAM.RED_TEAM;
    }

    public void Choose()
    {
        PlayerData player = GameManager.Instance.GetMasterPlayerData();
        player.Assign();
        player.GetInGameData().SetTeam(team);
        if (team == TEAM.RED_TEAM)
            player.selectframe = PrefabManager.GetInstance().GetPrefab("CS_Overlay_Red");// frameObj[(int)team];
        else if (team == TEAM.BLUE_TEAM)
            player.selectframe = PrefabManager.GetInstance().GetPrefab("CS_Overlay_Blue");// frameObj[(int)team];

        PlayerData player2 = GameManager.Instance.GetPlayer(PLAYER.PLAYER_TWO);
        player2.MakeCPU();
        //player2.Assign();
        player2.GetInGameData().SetTeam(opposingTeam);
        player2.selectframe = PrefabManager.GetInstance().GetPrefab("CS_Overlay_CPU");
    }
}
