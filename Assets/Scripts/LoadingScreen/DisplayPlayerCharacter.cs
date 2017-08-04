using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayPlayerCharacter : MonoBehaviour {

    public TEAM playerTeam;
    Text charName;
    Image charImage;

	// Use this for initialization
	void Start () {
        StartCoroutine(LateStart(0.01f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        charImage = GetComponent<Image>();
        charName = GetComponentInChildren<Text>();

        string charaName = GameManager.Instance.GetPlayer(playerTeam).GetInGameData().GetCharName();
        charName.text = charaName.ToUpper();
        charImage.sprite = CharacterManager.GetInstance().GetCharacterByName(charaName).GetCharArt();
    }

}
