using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour {

    public int healing = 50;
    public GameObject particles;
    

    // sound
    protected AudioSource audioSource;
    public AudioClip slurp;
    private float vol = 1.0f;
    private GameObject Hp;

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            audioSource = GetComponent<AudioSource>();

            //give colliding player HP
            GameObject Player = col.gameObject;
            col.gameObject.GetComponent<PlayerHealth>().PickUpHealth(healing);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameObject child = transform.FindChild("Hp").gameObject;
            child.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            if (Hp != null)
                {

                }
            Destroy(gameObject, 2);

            // play sound
            audioSource.PlayOneShot(slurp, vol);  
                      
            //create particles for health pickup
            GameObject HealthParticle = Instantiate(particles, Player.transform.position, Player.transform.rotation) as GameObject;
            HealthParticle.transform.parent = Player.transform;

        }
    }

}
