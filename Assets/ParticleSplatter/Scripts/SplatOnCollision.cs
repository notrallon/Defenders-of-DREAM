using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour {

	public ParticleSystem particleLauncher;
	public Gradient particleColorGradient;
	public ParticleDecalPool dropletDecalPool;

	List<ParticleCollisionEvent> collisionEvents;


	void Start () 
	{
		collisionEvents = new List<ParticleCollisionEvent> ();
        //Color color = particleLauncher.main.startColor;
        Debug.Log(particleLauncher.main.startColor);
	}

	void OnParticleCollision(GameObject other)
	{
		int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents (particleLauncher, other, collisionEvents);

        //Debug.Log(other.tag);

		int i = 0;
		while (i < numCollisionEvents) 
		{
            dropletDecalPool.ParticleHit(collisionEvents[i], particleLauncher.main.startColor.color);
            i++;
		}
	}
}