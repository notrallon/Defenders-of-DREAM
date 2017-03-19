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
    private void Start () {
        TriggeredLightObjects = GameObject.FindGameObjectsWithTag("SmallLights");

        DefaultColor = GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].color;
        DefaultEmission = GameObject.FindGameObjectWithTag("SmallLamps").GetComponent<Renderer>().materials[2].GetColor("_EmissionColor");
        YellowColor = YellowLight.color;
        YellowEmission = YellowLight.GetColor("_EmissionColor");

        SmallLamps = GameObject.FindGameObjectsWithTag("SmallLamps");
    }

    private void OnTriggerEnter(Collider col) {
        if (!col.gameObject.CompareTag("Player")) {
            return;
        }

        foreach (var t in SmallLamps) {
            t.GetComponent<Renderer>().materials[2].color = YellowColor;
            t.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", YellowEmission);
        }

        foreach (var t in TriggeredLightObjects) {
            t.GetComponent<Light>().intensity = 0;
        }
    }
}
