using UnityEngine;
using System.Collections;

public class SingletonScript : MonoBehaviour
{
    public static SingletonScript instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
	}
}
