using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour {

	public ParticleSystem particleLauncher;
	public Gradient particleColorGradient;
	public ParticleDecalPool[] dropletDecalPools;

    private int particleDecalPoolIndex = 0;
    private int particleIndex = 0;

    public Color PlayerColor;

    private ParticleDecalController m_ParticleDecalController;

	List<ParticleCollisionEvent> collisionEvents;

	void Start () {
		collisionEvents = new List<ParticleCollisionEvent> ();
	    m_ParticleDecalController = GameObject.FindGameObjectWithTag("DecalController").GetComponent<ParticleDecalController>();
	}

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        //m_ParticleDecalController.PlaceDecals(collisionEvents, particleLauncher.main.startColor.color);

        //ParticleSystem.Particle[] parts = new ParticleSystem.Particle[numCollisionEvents];

		int i = 0;
		while (i < numCollisionEvents) {
		    m_ParticleDecalController.PlaceDecals(collisionEvents[i], particleLauncher.main.startColor.color);
		    i++;
//            dropletDecalPools[particleDecalPoolIndex].ParticleHit(collisionEvents[i], particleLauncher.main.startColor.color);
//            i++;
//            particleIndex++;
//            if (particleIndex >= dropletDecalPools[particleDecalPoolIndex].maxDecals) {
//                particleDecalPoolIndex++;
//                particleIndex = 0;
//                if (particleDecalPoolIndex >= dropletDecalPools.Length) {
//                    particleDecalPoolIndex = 0;
//                }
//            }
		}
	}
}