using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTank : MonoBehaviour {
    [SerializeField]
    GameObject healthTank;

    const float SCALE_MIN = 0f;
    const float SCALE_MAX = 1f;

    private float scaleX;

    public void SetScale(float percent)
    {
        scaleX = percent / 100;

        scaleX = Mathf.Clamp(scaleX, SCALE_MIN, SCALE_MAX);

        Vector3 scale = healthTank.transform.localScale;
        scale.x = scaleX;
        healthTank.transform.localScale = scale;
    }
}
