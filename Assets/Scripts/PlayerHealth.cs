﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class PlayerHealth : MonoBehaviour {

    public float PlayerHP = 100;
    const float MAX_HEALTH = 100;
    const float DEATH_TRIGGER = 0;

    private float flashLength = 0.2f;   // set time length
    private float flashCounter; // countdown timer

    private Renderer rend; // this will render the flash
    private Color storedColor; // store current color


    public AudioClip takeDamage;
    AudioSource audio;


    public float GetPlayerHealth { get
        {
            return PlayerHP;
        }
    }

    

    // Use this for initialization
    private void Start () {

        audio = GetComponent<AudioSource>();

        rend = GetComponentInChildren<Renderer>(); // get renderer of first child
        storedColor = rend.material.GetColor("_Color");
    }
	
	// Update is called once per frame
    private void Update () {
        if (flashCounter > 0) // if flashcounter is more than 0
        {
            GetComponent<PlayerInput>().SetVibration(flashCounter);
            flashCounter -= Time.deltaTime; // start counting down
            if (flashCounter <= 0)  // when count down is finished
            {
                rend.material.SetColor("_Color", storedColor); // reset the color to original
                GetComponent<PlayerInput>().SetVibration(0);
            }
        }

        GetComponent<HealthTank>().SetScale(PlayerHP);
    }

    public void PickUpHealth (int healing)
    {
        PlayerHP += healing;
        if (PlayerHP > MAX_HEALTH) {
            PlayerHP = MAX_HEALTH;
        }
    }

    public void TakeDamage(float amount)
    {
        PlayerHP -= amount;

        audio.pitch = Random.Range(0.9f, 1.2f);
        audio.PlayOneShot(takeDamage, 2f);

        flashCounter = flashLength; // count down timer is set
        rend.material.SetColor("_Color", Color.red); // set material color to red

        if (PlayerHP <= 0) {
            PlayerHP = 0;
            GetComponent<PlayerInput>().SetVibration(0);
            flashCounter = 0f;
            GetComponent<Animator>().SetTrigger("Die"); // Play death animation
            gameObject.tag = "Untagged";
            GetComponent<PlayerInput>().enabled = false;
            rend.material.SetColor("_Color", storedColor); // reset the color to original
            //gameObject.SetActive(false); // deactivate the player object if health reaches 0
            GameController.Instance.UpdatePlayers();
        }
    }

    void DeathTrigger ()
    {
        if(GetPlayerHealth <= DEATH_TRIGGER)
        {
            
        }

    }
}
