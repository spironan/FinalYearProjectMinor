using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour {
    public MovieTexture movie;
    RawImage rawImage;
	// Use this for initialization
	void Start () {
        rawImage = GetComponent<RawImage>();
        rawImage.texture = movie;
        movie.Play();
        movie.loop = true;
    }
}
