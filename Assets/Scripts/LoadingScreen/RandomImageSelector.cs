using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomImageSelector : MonoBehaviour 
{
    public Sprite[] images;
	// Use this for initialization
	void Start () 
    {
        gameObject.GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
	}
}
