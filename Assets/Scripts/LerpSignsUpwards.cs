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

    // Screenshake
    [SerializeField]
    [Range(0.1f, 5f)]
    private float m_ShakeDuration = 1f;
    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float m_ShakeMagnitude = 0.2f;

    private AudioSource m_AudioSource;
    private AudioClip m_AudioClip;


    void Start () {
        m_DefaultPos = gameObject.transform.position;
        m_TargetPos = gameObject.transform.position;
        m_TargetPos.y += amountToMoveY;

        eruptionPart = Resources.Load("Particle Systems/ParticleSpawnSigns") as GameObject;

        m_AudioClip = Resources.Load("Sound/SoundEffects/Long Impacts/signs_rising_from_ground_2") as AudioClip;
        m_AudioSource = GetComponent<AudioSource>();
    }
	


//	void Update () {
//
//        if(Input.GetKeyDown(KeyCode.H) && !activate)
//        {
//            ActivateSigns();
//            activate = true;
//        }
//
//    }


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

        Camera.main.GetComponent<CameraShaker>().Shake(m_ShakeDuration, m_ShakeMagnitude);
        m_AudioSource.PlayOneShot(m_AudioClip, 0.5f);
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
