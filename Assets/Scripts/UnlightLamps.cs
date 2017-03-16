using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlightLamps : MonoBehaviour {

    //private SphereCollider LightCollider;
    private Light TriggeredLight;
    private GameObject[] TriggeredLightObjects;

    private GameObject[] SmallLamps;
    private Material[] Materials;
    public Material YellowLight;

    private Color YellowColor;
    private Color YellowEmission;
    private Color DefaultColor;
    private Color DefaultEmission;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (TriggeredLightObjects != null)
        {
            foreach (GameObject Light in TriggeredLightObjects)
            {
                TriggeredLight = Light.GetComponent<Light>();
                TriggeredLight.intensity = 0;
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

                //LightCollider = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<SphereCollider>();
                TriggeredLightObjects = GameObject.FindGameObjectsWithTag("SmallLights");

                GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].color = YellowLight.color;
                GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", YellowLight.GetColor("_EmissionColor"));
                //Materials[2] = YellowLight;


                //GreatLamp = GameObject.FindGameObjectsWithTag("GreatLamp");

                //foreach (GameObject Lamp in GreatLamp)
                //{
                //    LightCollider = GetComponent<SphereCollider>();
                //}

            }
        // Set colors
        DefaultColor = GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].color;
        DefaultEmission = GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].GetColor("_EmissionColor");
        YellowColor = YellowLight.color;
        YellowEmission = YellowLight.GetColor("_EmissionColor");

        // Start coroutine
        InvokeRepeating("LampFadeIn", 0f, 0.01f);
    }

    private void LampFadeIn()
    {

        SmallLamps = GameObject.FindGameObjectsWithTag("SmallLamps");
        foreach (GameObject Lamp in SmallLamps)
        {
            Lamp.GetComponent<Renderer>().materials[2].color = Color.Lerp(DefaultColor, YellowColor, 100);
            Lamp.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.Lerp(DefaultEmission, YellowEmission, 100));
        }
    }
}
