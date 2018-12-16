using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour {

    private ParticleSystem ParSystem;
    private ParticleSystem.Particle[] TheParticles;
    public GameObject MainChar;
    private Vector3 MainCharPos;
    private float speed = 1.0f;

    // Use this for initialization
    void Start () {
        MainCharPos = new Vector3(MainChar.transform.position.x, MainChar.transform.position.y);
	}

    // Update is called once per frame
    private void LateUpdate() {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = ParSystem.GetParticles(TheParticles);

        // Change only the particles that are alive
        for (int i = 0; 0 < numParticlesAlive; i++) {
            //Debug.Log(TheParticles[i].position);
            /*if (TheParticles[i].position != null) {
                TheParticles[i].position = MainCharPos;
            }*/
            
        }
	}

    void InitializeIfNeeded() {
        if (ParSystem == null)
            ParSystem = GetComponent<ParticleSystem>();

        if (TheParticles == null || TheParticles.Length < ParSystem.main.maxParticles)
            TheParticles = new ParticleSystem.Particle[ParSystem.main.maxParticles];
    }
}
