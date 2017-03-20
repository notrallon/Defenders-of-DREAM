using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSignsUpwards : MonoBehaviour {


    public bool activate = false;



    [SerializeField]
    private float amountToMoveY = 3f;
    [SerializeField]
    private float smooth = 0.2f;

    private bool spawnParticles;
    GameObject eruptionPart;

    private float m_LerpTime = 5f;
    private float m_CurrentLerpTime;

    private Vector3 m_DefaultPos;
    // Handle the signs rising
    private Vector3 m_TargetPos;

    void Start () {
        m_DefaultPos = gameObject.transform.position;
        m_TargetPos = gameObject.transform.position;
        m_TargetPos.y += amountToMoveY;

        eruptionPart = Resources.Load("Particle Systems/ParticleSpawnSigns") as GameObject;
    }
	
/*

	void Update () {

        if(Input.GetKeyDown(KeyCode.H) && !activate)
        {
            ActivateSigns();
            activate = true;
        }
/*
        if (activate)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_TargetPos, Time.deltaTime * smooth);
  
            // Activate the un-seen sign objects
            foreach(Transform child in transform)
            {
                if((!child.gameObject.activeSelf) && (!spawnParticles))
                {
                    child.gameObject.SetActive(true);
                    var rockShower = Instantiate(eruptionPart, child.gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
                    Destroy(rockShower, 4f);
                }
            }
            spawnParticles = true;
        }#1#
    }
*/

    public void ActivateSigns() {
        InvokeRepeating("LerpSigns", 0, 0.01f);

        foreach (Transform child  in transform) {
            if (!child.gameObject.activeSelf && !spawnParticles) {
                child.gameObject.SetActive(true);
                var rockShower = Instantiate(eruptionPart, child.gameObject.transform.position,
                    Quaternion.Euler(-90, 0, 0));
                Destroy(rockShower, 4f);
            }
        }
        spawnParticles = true;
    }

    private void LerpSigns() {
        m_CurrentLerpTime += Time.deltaTime;

        var t = m_CurrentLerpTime / m_LerpTime;

        transform.position = Vector3.Lerp(m_DefaultPos, m_TargetPos, t);

        if (m_CurrentLerpTime > m_LerpTime) {
            CancelInvoke("LerpSigns");
        }
    }
}
