﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : Interactable {

    private SphereCollider LightCollider;

    private Light TriggeredLight;
    private GameObject TriggeredLightObject;
    private GameObject[] GreatLamp;
    private Material[] Materials;
    public Material YellowLight;

    private Color YellowColor;
    private Color YellowEmission;
    private Color DefaultColor;
    private Color DefaultEmission;

    private float FadeInTime = 7f;
    private float CurrentFadeInTime;
	
	// Update is called once per frame
	void Update () {
		if (LightCollider != null)
        {
            if (LightCollider.radius < 100)
            {
                LightCollider.radius += 15*Time.deltaTime;
            }
        }

        if (TriggeredLightObject != null)
        {
            TriggeredLight = TriggeredLightObject.GetComponent<Light>();
            TriggeredLight.intensity += 0.5f * Time.deltaTime;

            if (TriggeredLight.range < 30)
            {
                TriggeredLight.range += 5 * Time.deltaTime;
            }
        }
	}

    public override void Interact()
    {
        LightCollider = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<SphereCollider>();
        TriggeredLightObject = GameObject.FindGameObjectWithTag("TriggeredLight");

        // Set colors
        DefaultColor = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].color;
        DefaultEmission = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].GetColor("_EmissionColor");
        YellowColor = YellowLight.color;
        YellowEmission = YellowLight.GetColor("_EmissionColor");

        // Start coroutine
        InvokeRepeating("LampFadeIn", 0f, 0.01f);
    }

    private void LampFadeIn()
    {
        CurrentFadeInTime += Time.deltaTime;
        float percentage = CurrentFadeInTime / FadeInTime;
        
        GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].color = Color.Lerp(DefaultColor, YellowColor, percentage);
        GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.Lerp(DefaultEmission, YellowEmission, percentage));
    }

   /* void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || (Input.GetKeyDown(KeyCode.E)))
            {

                LightCollider = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<SphereCollider>();
                TriggeredLightObject = GameObject.FindGameObjectWithTag("TriggeredLight");

                GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].color = YellowLight.color;
                GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", YellowLight.GetColor("_EmissionColor"));
                //Materials[2] = YellowLight;


                //GreatLamp = GameObject.FindGameObjectsWithTag("GreatLamp");

                //foreach (GameObject Lamp in GreatLamp)
                //{
                //    LightCollider = GetComponent<SphereCollider>();
                //}

            }
        }
    }*/
}
