using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour {

    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem particleDecalSystem;
    
    public int maxDecals = 100;
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;

	// Use this for initialization
	void Start () {
        particleDecalSystem = GetComponent<ParticleSystem>();
        particleData = new ParticleDecalData[maxDecals];
        particles = new ParticleSystem.Particle[maxDecals];

        for (var i = 0; i < maxDecals; i++) {
            particleData[i] = new ParticleDecalData();
        }
	}

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient) {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Color color) {
        SetParticleData(particleCollisionEvent, color);
        DisplayParticles();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient) {
        if (particleDecalDataIndex >= maxDecals) {
            particleDecalDataIndex = 0;
        }

        // Record collision position, rotation, size and color
        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0f, 360f);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f, 1f));

        particleDecalDataIndex++;
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Color color) {
        if (particleDecalDataIndex >= maxDecals) {
            particleDecalDataIndex = 0;
        }

        // Record collision position, rotation, size and color
        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Vector3.zero;//Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0f, 360f);
        particleRotationEuler.x = 90;
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = color;

        particleDecalDataIndex++;
    }

    void DisplayParticles() {
        // Read over our array of decaldata and turn it into an array of particles
        for (int i = 0; i < particleData.Length; i++) {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }
        particleDecalSystem.SetParticles(particles, particles.Length);
    }
}
