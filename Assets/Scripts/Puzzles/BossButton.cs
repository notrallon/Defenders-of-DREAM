﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButton : Interactable {
    private bool m_IsUnlocked;

    public bool IsActivated;

    private Material[] m_Materials;

    [SerializeField] private BossLidController m_BossLid;

    private void Awake() {
        Renderer rend = GetComponent<Renderer>();

        m_BossLid = GetComponentInParent<BossLidController>();

        m_Materials = rend.materials;

        m_Materials[1].SetColor("_EmissionColor", Color.red);
    }

    public override void Interact() {
        if (!m_IsUnlocked || IsActivated) {
            return;
        }

        // Set isactivated to true and update the lids buttons to check if the lid should open.
        IsActivated = true;
        m_BossLid.UpdateActivatedButtons();

        Destroy(GetComponent<BoxCollider>());
        //Get the material on the GameObject
        Renderer rend = GetComponent<Renderer>();

        rend.material.shader = Shader.Find("Standard");
        rend.material.SetColor("_EmissionColor", Color.cyan);

        GetComponentInParent<BossLinesChangeColor>().Activate(2f);
    }

    public void Unlock() {
        if (m_IsUnlocked) {
            return;
        }

        m_Materials[1].SetColor("_EmissionColor", Color.green);

        for (int i = 0; i < m_Materials.Length; i++) {
            m_DefaultColours[i] = m_Materials[i].color;
            m_DefaultEmissionColours[i] = m_Materials[i].GetColor("_EmissionColor");
        }

        m_IsUnlocked = true;
    }
}
