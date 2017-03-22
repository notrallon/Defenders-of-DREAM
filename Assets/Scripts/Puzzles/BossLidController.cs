﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLidController : MonoBehaviour {

    [SerializeField] private GameObject[] m_Buttons;

    public BossButton[] m_BossButtons;

    private const float OPEN_LID_TIME = 5f;
    private float m_CurrentOpenLidTime;

    private Vector3 m_LidStartPos;
    private Vector3 m_LidTargetPos;

	// Use this for initialization
    private void Start () {
        m_Buttons = GameObject.FindGameObjectsWithTag("BossLidButton");

		m_BossButtons = new BossButton[m_Buttons.Length];

        for (var i = 0; i < m_BossButtons.Length; i++) {
            m_BossButtons[i] = m_Buttons[i].GetComponent<BossButton>();
        }

        m_LidStartPos = transform.position;
        m_LidTargetPos = m_LidStartPos;
        m_LidTargetPos.y += 10;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            InvokeRepeating("OpenLid", 0.1f, 0.01f);
        }
    }

    public void ActivateButton(int index) {

    }

    public void UpdateActivatedButtons() {
        var amountActivated = 0;
        for (var i = 0; i < m_BossButtons.Length; i++) {
            if (m_BossButtons[i].IsActivated) {
                amountActivated++;
            }
        }

        if (amountActivated == m_BossButtons.Length) {
            InvokeRepeating("OpenLid", 0.1f, 0.01f);
        }
    }

    private void OpenLid() {
        m_CurrentOpenLidTime += Time.deltaTime;

        var t = m_CurrentOpenLidTime / OPEN_LID_TIME;

        t = t * t;

        transform.position = Vector3.Lerp(m_LidStartPos, m_LidTargetPos, t);

        if (m_CurrentOpenLidTime > OPEN_LID_TIME) {
            Destroy(gameObject);
            CancelInvoke("OpenLid");
        }
    }

}
