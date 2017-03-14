using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{

    private Transform WatchTarget;
    private List <Transform> _WatchTargets;
    public LayerMask OccluderMask;

    //private GameObject[] tempObject;

    //This is the material with the Transparent/Diffuse With Shadow shader
    public Material HiderMaterial;

    private Material[] intMaterials;

    private Dictionary<Transform, Material> _LastTransforms;

    void Start()
    {
        _LastTransforms = new Dictionary<Transform, Material>();
        _WatchTargets = new List<Transform>();

        var tempObject = GameController.Instance.PlayerInstances;

        //Put all players into a list
        foreach(Transform Player in tempObject)
        {
            //WatchTarget = Player.transform;
            _WatchTargets.Add(Player);
        }
    }

    void Update()
    {

        //reset and clear all the previous objects
        if (_LastTransforms.Count > 0)
        {
            foreach (Transform t in _LastTransforms.Keys)
            {
                t.GetComponent<Renderer>().material = _LastTransforms[t];
            }
            _LastTransforms.Clear();
        }

            //check for every player
        foreach (Transform Player in _WatchTargets)
        {
            //Cast a ray from this object's transform to the watch target's transform.

            RaycastHit[] hits = Physics.RaycastAll(
                transform.position,
                Player.transform.position - transform.position,
                Vector3.Distance(Player.transform.position, transform.position),
                OccluderMask
            );


            //Loop through all overlapping objects and disable their mesh renderer
            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.transform != Player && hit.collider.transform.root != Player && hit.collider.tag != "Player")
                    {
                        //for (int i = 0; i < hit.collider.gameObject.GetComponent<Renderer>().materials.Length; i++) {
                        //    _LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().materials[i]);                        

                        //hit.collider.gameObject.GetComponent<Renderer>().material = HiderMaterial;
                        //}

                        intMaterials = new Material[hit.collider.gameObject.GetComponent<Renderer>().materials.Length];

                        for (int i = 0; i < intMaterials.Length; i++)
                        {
                            _LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().materials[i]);
                            hit.collider.gameObject.GetComponent<Renderer>().material = HiderMaterial;

                            //_LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material);
                            //hit.collider.gameObject.GetComponent<Renderer>().material = HiderMaterial;
                        }


                    }
                }
            }
        }
    }
}
