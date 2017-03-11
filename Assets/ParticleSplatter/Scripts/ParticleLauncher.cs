using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public ParticleDecalPool splatDecalPool;
    public ParticleSystem decalParticles;

    List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
	}

    private void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++) {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]); // Pass in the collision event so that we can set position and rotation
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent) {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
        splatterParticles.Emit(1);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Fire1")) {
            ParticleSystem.MainModule psMain = particleLauncher.main; // Reference to the main module
            psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f)); // Pick a random color from our gradient

            particleLauncher.Emit(1);
        }
	}
}
