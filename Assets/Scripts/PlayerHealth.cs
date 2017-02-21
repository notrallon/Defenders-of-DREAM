using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float PlayerHP = 100;
    const float MAX_HEALTH = 100;
    const float DEATH_TRIGGER = 0;

    public float GetPlayerHealth { get
        {
            return PlayerHP;
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PickUpHealth ()
    {
        if (PlayerHP > MAX_HEALTH)
            PlayerHP = MAX_HEALTH;
        
    }

    public void TakeDamage(float amount)
    {
        PlayerHP -= amount;
    }

    void DeathTrigger ()
    {
        if(GetPlayerHealth <= DEATH_TRIGGER)
        {

        }

    }
}
