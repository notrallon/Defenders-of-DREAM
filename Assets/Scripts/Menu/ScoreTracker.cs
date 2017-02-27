using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    public static int score;
    // Use this for initialization
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update () {
        text.text = "Score: " + score;
	}
}
