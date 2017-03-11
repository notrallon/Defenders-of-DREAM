using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    List<ParticleCollisionEvent> m_CollisionEvents;

    [SerializeField] private ParticleSystem m_SplatterParticles;
    [SerializeField] private ParticleSystem m_SplatterDecalParticles;
    [SerializeField] private ParticleDecalPool m_DropletDecalPool;

    private Color m_PlayerColor;

    // Use this for initialization
    void Start () {
        m_CollisionEvents = new List<ParticleCollisionEvent>();
	}
	
    public void EmitSplatterAtLocation(Transform location, Color color = default(Color)) {
        m_SplatterParticles.transform.position = location.position;
        ParticleSystem.MainModule psMain = m_SplatterParticles.main;
        m_PlayerColor = color;
        psMain.startColor = m_PlayerColor;
        m_SplatterParticles.GetComponent<SplatOnCollision>().PlayerColor = m_PlayerColor;
        
        m_SplatterParticles.transform.rotation = Quaternion.LookRotation(location.rotation.eulerAngles);
        m_SplatterParticles.Emit(1);
    }

    /*private void OnParticleCollision(GameObject other) {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(m_SplatterParticles, other, m_CollisionEvents);

        for (var i = 0; i < numCollisionEvents; i++) {
            m_DropletDecalPool.ParticleHit(m_CollisionEvents[i], m_PlayerColor);
        }
    }*/
}
