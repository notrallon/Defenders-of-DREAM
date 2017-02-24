using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float PlayerHP = 100;
    const float MAX_HEALTH = 100;
    const float DEATH_TRIGGER = 0;

    public float flashLength;   // set time length
    private float flashCounter; // countdown timer

    private Renderer rend; // this will render the flash
    private Color storedColor; // store current color

    public float GetPlayerHealth { get
        {
            return PlayerHP;
        }
    }



    // Use this for initialization
    void Start () {

        rend = GetComponentInChildren<Renderer>(); // get renderer of first child
        storedColor = rend.material.GetColor("_Color");
    }
	
	// Update is called once per frame
	void Update () {
		
        if(PlayerHP <= 0)
        {
            gameObject.SetActive(false); // deactivate the player object if health reaches 0
            Camera.main.GetComponent<CoopCamera>().UpdatePlayers();
        }

        if (flashCounter > 0) // if flashcounter is more than 0
        {
            flashCounter -= Time.deltaTime; // start counting down
            if (flashCounter <= 0)  // when count down is finished
            {
                rend.material.SetColor("_Color", storedColor); // reset the color to original
            }
        }
    }

    void PickUpHealth ()
    {
        if (PlayerHP > MAX_HEALTH)
            PlayerHP = MAX_HEALTH;
        
    }

    public void TakeDamage(float amount)
    {
        PlayerHP -= amount;

        flashCounter = flashLength; // count down timer is set
        rend.material.SetColor("_Color", Color.red); // set material color to red

        Debug.Log("I took damage. I now have " + PlayerHP + " HP");
    }

    void DeathTrigger ()
    {
        if(GetPlayerHealth <= DEATH_TRIGGER)
        {
            
        }

    }
}
