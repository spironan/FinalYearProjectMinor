using UnityEngine;
using System.Collections;

public class ParticleBehaviour : MonoBehaviour {

    ParticleSystem systemForParticles;
	// Use this for initialization
	void Start () {
        systemForParticles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(systemForParticles.time >= systemForParticles.duration)
        {
            Destroy(gameObject);
        }
	}
}
