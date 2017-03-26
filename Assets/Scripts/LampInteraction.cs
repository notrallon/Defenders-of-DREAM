using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : Interactable {

    private SphereCollider LightCollider;

    private Light TriggeredLight;
    private GameObject TriggeredLightObject;
    private GameObject[] GreatLamp;
    private Material[] Materials;
    public Material YellowLight;

    private GameObject m_GreatLamp;

    private GameObject[] SmallLamps;
    private GameObject[] TriggeredSmallLightObjects;

    private Color YellowColor;
    private Color YellowEmission;
    private Color DefaultColor;
    private Color DefaultEmission;

    private const float FADE_IN_TIME = 7.5f;
    private float m_CurrentFadeInTime;

    private float BIG_LIGHT_TARGET_INTENSITY = 100f;
    private float m_BigLightDefaultIntensity;

    private const float COLLIDER_TARGET_RADIUS = 100f;
    private float m_DefaultColliderRadius;

    private const float BIG_LIGHT_TARGET_RANGE = 30f;
    private float m_BigLightDefaultRange;

    private const float SMALL_LIGHT_TARGET_INTENSITY = 1.25f;
    private float m_SmallLightDefaultIntensity;

    //private Color m_PlayerColor;

    private ParticleSystem m_PlayerParticleSystem;


    protected override void Start() {
        base.Start();
        // Cache objects
        m_GreatLamp                    = GameObject.FindGameObjectWithTag("GreatLamp");
        LightCollider                  = m_GreatLamp.GetComponent<SphereCollider>();
        TriggeredLightObject           = GameObject.FindGameObjectWithTag("TriggeredLight");
        TriggeredSmallLightObjects     = GameObject.FindGameObjectsWithTag("SmallLights");
        SmallLamps                     = GameObject.FindGameObjectsWithTag("SmallLamps");

        // Set up default and target values
        m_BigLightDefaultIntensity     = TriggeredLightObject.GetComponent<Light>().intensity;
        m_BigLightDefaultRange         = TriggeredLightObject.GetComponent<Light>().range;
        m_DefaultColliderRadius        = LightCollider.radius;

        // Set colors
        DefaultColor                   = m_GreatLamp.GetComponent<Renderer>().materials[2].color;
        DefaultEmission                = m_GreatLamp.GetComponent<Renderer>().materials[2].GetColor("_EmissionColor");
        YellowColor                    = YellowLight.color;
        YellowEmission                 = YellowLight.GetColor("_EmissionColor");
    }

    public override void Interact() {
        Destroy(GetComponent<BoxCollider>());

        m_GreatLamp.GetComponent<GhostKillerLight>().PlayerParticleSystem =
            Player.GetComponent<PlayerInput>().PlayerParticleSystem;
        m_GreatLamp.GetComponent<GhostKillerLight>().PlayerColor = Player.GetComponent<PlayerInput>().PlayerColor;

        // Start coroutine
        InvokeRepeating("LampFadeIn", 0f, 0.01f);
    }

    private void LampFadeIn() {
        // Add deltatime
        m_CurrentFadeInTime += Time.deltaTime;

        // Calculate the percentage of how far we are through
        var percentage = m_CurrentFadeInTime / FADE_IN_TIME;

        // Lerp the light colliders radius
        LightCollider.radius = Mathf.Lerp(m_DefaultColliderRadius, COLLIDER_TARGET_RADIUS, percentage);

        // Lerp the light intensity of the big light
        TriggeredLightObject.GetComponent<Light>().intensity = Mathf.Lerp(m_BigLightDefaultIntensity,
            BIG_LIGHT_TARGET_INTENSITY, percentage);

        // Lerp the light range of the big light
        TriggeredLightObject.GetComponent<Light>().range =
            Mathf.Lerp(m_BigLightDefaultRange, BIG_LIGHT_TARGET_RANGE, percentage);


        // Fade in the large lampost color
        m_GreatLamp.GetComponent<Renderer>().materials[2].color = Color.Lerp(DefaultColor, YellowColor, percentage);
        m_GreatLamp.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.Lerp(DefaultEmission, YellowEmission, percentage));

        // Fade in the small lights
        foreach (var tLight in TriggeredSmallLightObjects) {
            tLight.GetComponent<Light>().intensity = Mathf.Lerp(0, SMALL_LIGHT_TARGET_INTENSITY, percentage);
            tLight.GetComponent<Light>().range = Mathf.Lerp(0, 10f, percentage);
        }

        // Fade in all the small lamps colors
        foreach (GameObject Lamp in SmallLamps) {
            Lamp.GetComponent<Renderer>().materials[2].color = Color.Lerp(DefaultColor, YellowColor, percentage);
            Lamp.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.Lerp(DefaultEmission, YellowEmission, percentage));
        }

        // Cancel the coroutine once we're done
        if (m_CurrentFadeInTime > FADE_IN_TIME) {
            CancelInvoke("LampFadeIn");
        }
    }
}
