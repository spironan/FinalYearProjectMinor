using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillDisplayBaseScript : MonoBehaviour 
{
    Image overlay, icon , buttonInput;

	// Use this for initialization
	void Awake () {
        overlay = GetComponent<Image>();
        icon = transform.GetChild(0).GetComponent<Image>();
        buttonInput = transform.GetChild(1).GetComponent<Image>();
	}

    public void SetIcon(SkillProfile skill )
    {
        icon.sprite = skill.skillIcon;
        //if (cost <= 0.25f)
        //{
        //    overlay.color = Color.red;
        //}
        //else if (cost <= 0.5f)
        //{
        //    overlay.color = Color.yellow;
        //}
        //else if (cost <= 0.75f)
        //{
        //    overlay.color = Color.cyan; //new Color(255, 64, 35);
        //}
        //else
        //{
        //    overlay.color = Color.green;
        //}
    }

    public void SetInput(Sprite inputSprite)
    {
        this.buttonInput.sprite = inputSprite;
    }
}
