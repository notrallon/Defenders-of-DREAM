﻿using UnityEngine;

public class CoopCamera : MonoBehaviour {
    private Transform[] m_PlayerTransforms;

    public float OffsetY = 2.0f;
    public float MinDistance = 7.5f;

    [SerializeField]
    private float m_CameraMaxDistance = 20.0f;
    [SerializeField]
    private float m_CameraDistanceDivider = 1.5f;
    [SerializeField]
    private float m_CameraSmoothing = 10.0f;
    
    private float m_MinX;
    private float m_MaxX;
    private float m_MinZ;
    private float m_MaxZ;

    private void Start() {
        UpdatePlayers();
    }

    private void LateUpdate() {
        if (m_PlayerTransforms.Length == 0) {
            return;
        }
        m_MinX = m_MaxX = m_PlayerTransforms[0].position.x;
        m_MinZ = m_MaxZ = m_PlayerTransforms[0].position.z;

        for (var i = 1; i < m_PlayerTransforms.Length; i++) {
            if (m_PlayerTransforms[i].position.x < m_MinX) {
                m_MinX = m_PlayerTransforms[i].position.x;
            } else if (m_PlayerTransforms[i].position.x > m_MaxX) {
                m_MaxX = m_PlayerTransforms[i].position.x;
            }

            if (m_PlayerTransforms[i].position.z < m_MinZ) {
                m_MinZ = m_PlayerTransforms[i].position.z;
            } else if (m_PlayerTransforms[i].position.z > m_MaxZ) {
                m_MaxZ = m_PlayerTransforms[i].position.z;
            }
        }

        var xMiddle = (m_MinX + m_MaxX) / 2.0f;
        var zMiddle = (m_MinZ + m_MaxZ) / 2.0f;
        var distance = Mathf.Max(m_MaxX - m_MinX, m_MaxZ - m_MinZ);

        if (distance < MinDistance) {
            distance = MinDistance;
        } else if (distance > m_CameraMaxDistance) {
            distance = m_CameraMaxDistance;
        }
        
        distance /= m_CameraDistanceDivider;

        Vector3 targetCamPos = new Vector3(xMiddle, distance, zMiddle - distance);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, m_CameraSmoothing * Time.deltaTime);
    }

    public void UpdatePlayers() {
        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        m_PlayerTransforms = new Transform[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++)
        {
            m_PlayerTransforms[i] = allPlayers[i].transform;
        }
    }
}
