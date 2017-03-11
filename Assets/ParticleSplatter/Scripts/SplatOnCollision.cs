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

	List<ParticleCollisionEvent> collisionEvents;

	void Start () 
	{
		collisionEvents = new List<ParticleCollisionEvent> ();
	}

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        //ParticleSystem.Particle[] parts = new ParticleSystem.Particle[numCollisionEvents];

		int i = 0;
		while (i < numCollisionEvents) 
		{
            dropletDecalPools[particleDecalPoolIndex].ParticleHit(collisionEvents[i], particleLauncher.main.startColor.color);
            i++;
            particleIndex++;
            if (particleIndex >= dropletDecalPools[particleDecalPoolIndex].maxDecals) {
                particleDecalPoolIndex++;
                if (particleDecalPoolIndex >= dropletDecalPools.Length) {
                    particleDecalPoolIndex = 0;
                }
            }
		}
	}
}