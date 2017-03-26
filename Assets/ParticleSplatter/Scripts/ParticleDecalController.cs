using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleDecalController : MonoBehaviour {

    private ParticleDecalPool[] DropletDecalPools;

    private int m_ParticleDecalPoolIndex = 0;
    private int m_ParticleIndex = 0;

	// Use this for initialization
	void Start () {
	    var dropletSystems = GetComponentsInChildren<ParticleDecalPool>();
	    DropletDecalPools = dropletSystems.ToArray();
	    Debug.Log(DropletDecalPools.Length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceDecals(ParticleCollisionEvent particleCollisionEvent, Color decalColor) {
        m_ParticleIndex++;
        DropletDecalPools[m_ParticleDecalPoolIndex].ParticleHit(particleCollisionEvent, decalColor);

        if (m_ParticleIndex >= DropletDecalPools[m_ParticleDecalPoolIndex].maxDecals) {
            m_ParticleIndex = 0;
            m_ParticleDecalPoolIndex++;
            if (m_ParticleDecalPoolIndex >= DropletDecalPools.Length) {
                m_ParticleDecalPoolIndex = 0;
            }
        }
    }
}
