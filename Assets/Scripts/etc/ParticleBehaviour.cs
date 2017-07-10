using UnityEngine;
using System.Collections;

public class ParticleBehaviour : MonoBehaviour {

    ParticleSystem systemForParticles;
    float time;
	// Use this for initialization
	void Start () {
        systemForParticles = GetComponent<ParticleSystem>();
        time = systemForParticles.time;

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= systemForParticles.duration + 0.5f)
        {
            Destroy(gameObject);
        }
	}
}
