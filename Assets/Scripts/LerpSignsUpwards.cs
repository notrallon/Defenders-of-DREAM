using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSignsUpwards : MonoBehaviour {


    public bool activate;

    private Vector3 m_TargetPos;

    [SerializeField]
    private float amountToMoveY = 3f;
    [SerializeField]
    private float smooth = 0.2f;


    // Use this for initialization
    void Start () {
        
        m_TargetPos = gameObject.transform.position;
        m_TargetPos.y += amountToMoveY;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(activate)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_TargetPos, Time.deltaTime * smooth);
        }

	}
}
