using UnityEngine;
using System.Collections;

public class FlipObjectScript : MonoBehaviour
{
    public bool flipX, flipY, flipZ;
    Vector3 newScale;

    public void Flip()
    {
        if(flipX)
            newScale.x = -newScale.x;
        if(flipY)
            newScale.y = -newScale.y;
        if(flipZ)
            newScale.z = -newScale.z;

        transform.localScale = newScale;
    }

	// Use this for initialization
	void Start () {
        newScale = transform.localScale;
    }
	
}
