using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public float damage;
    
    GameObject enemyObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyObject = col.gameObject;
            var script = enemyObject.GetComponent<IEnemy>();
            script.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

}