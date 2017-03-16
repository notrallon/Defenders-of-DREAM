using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSignsUpwards : MonoBehaviour {


    public bool activate = false;


    // Handle the signs rising
    private Vector3 m_TargetPos;

    [SerializeField]
    private float amountToMoveY = 3f;
    [SerializeField]
    private float smooth = 0.2f;


    ParticleSystem[] childrenInParticleSystems;
    

    // Use this for initialization
    void Start () {
        
        m_TargetPos = gameObject.transform.position;
        m_TargetPos.y += amountToMoveY;


        childrenInParticleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();

        
    }
	
	// Update is called once per frame
	void Update () {

        if (activate)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_TargetPos, Time.deltaTime * smooth);



            foreach (ParticleSystem childPS in childrenInParticleSystems)
            {
                // Get the emission module of the current child particle system [childPS]
                ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
                // Disable the childs emission module
                childPSEmissionModule.enabled = true;

                // Get all particle systems from the children of the current child [childPS]
                ParticleSystem[] grandchildrenParticleSystems = childPS.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem grandchildPS in grandchildrenParticleSystems)
                {
                    // Get the emission module from the particle system of the current grandchild
                    ParticleSystem.EmissionModule grandchildPSEmissionModule = grandchildPS.emission;

                    // Enable the grandchilds emission module
                    grandchildPSEmissionModule.enabled = true;
                }
            }
        }
	}
}
