using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSlot : MonoBehaviour
{
    public GameObject up, down, left, right;
    Image icon;

    void Start()
    {
        up = down = left = right = null;
        icon = GetComponent<Image>();
    }

    public void SetImage(Image image)
    {
        icon.sprite = image.sprite;
    }

}
