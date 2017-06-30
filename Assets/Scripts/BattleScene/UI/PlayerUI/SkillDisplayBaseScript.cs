using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillDisplayBaseScript : MonoBehaviour 
{
    Image overlay, icon;

	// Use this for initialization
	void Awake () {
        overlay = GetComponent<Image>();
        icon = GetComponentInChildren<Image>();
	}

    public void SetIcon(Sprite iconSprite)
    {
        icon.sprite = iconSprite;
    }
}
