using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Renderer))]
public class LerpMatColor : MonoBehaviour
{
    public Material[] m;

    Color color;

    float t = 0;
    //[SerializeField]
    float i = 0.05f;
    bool change = false;

    void Awake()
    {
        color = m[0].color;
    }

    void Update()
    {
       GetComponent<Renderer>().material.color = Color.Lerp(m[0].color, m[1].color, t);
        if (!change)
            t += i;
        else
            t -= i;
        if (t >= 1)
            change = true;
        if (t <= 0)
            change = false;
    }
}