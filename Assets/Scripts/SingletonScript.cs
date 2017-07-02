using UnityEngine;
using System.Collections;

public class SingletonScript : MonoBehaviour 
{
    private static SingletonScript instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
	}

    public static SingletonScript Instance
    {
        get
        {
            return instance;
        }
    }
}
