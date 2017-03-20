using UnityEngine;
using System.Linq;

public class HideObjectsFromPlayer : MonoBehaviour {
    public Transform WatchTarget;
    public LayerMask OccluderMask;

    //This is the material with the Transparent/Diffuse With Shadow shader
    public Material HiderMaterial;

//    private Material[] intMaterials;
//    private Dictionary<Transform, Material[]> _LastTransforms;

    void Start() {
        // The camera is our watchtarget
        WatchTarget = Camera.main.transform;
    }

    private void LateUpdate()
    {
        //Cast a ray from this object's transform to the watch target's transform.
        RaycastHit[] hits = Physics.RaycastAll(
            transform.position,
            WatchTarget.transform.position - transform.position,
            Vector3.Distance(WatchTarget.transform.position, transform.position),
            OccluderMask
        );

        // No need to go further if nothing was hit
        if (hits.Length <= 0) {
            return;
        }

        //Loop through all overlapping objects and change the material
        foreach (RaycastHit hit in hits) {
            var currentObj = hit.collider.gameObject;
            // If the collision is with the watchtarget or if the collision isn't with a object with the tag
            // "CanTransparent" we continue to the next index in our loop
            if (currentObj.transform == WatchTarget || currentObj.transform.root == WatchTarget ||
                (!currentObj.CompareTag("CanTransparent"))) {
                continue;
            }
            // Do not add if the material already is the hidermaterial or if it's already in the dictionary
            if (currentObj.GetComponent<Renderer>().materials.Contains(HiderMaterial) ||
                HideObjectsController.Instance.ObjectsToHide.ContainsKey(currentObj.transform)) {
                continue;
            }

            // Get the amount of materials in the colliding object
            var matLength = currentObj.GetComponent<Renderer>().materials.Length;

            // Add the object to the dictionary
            HideObjectsController.Instance.ObjectsToHide.Add(currentObj.transform, new Material[matLength]);
            for (int i = 0; i < matLength; i++) {
                // Update our current index in the dictionary and cache all the materials of the object
                HideObjectsController.Instance.ObjectsToHide[currentObj.transform].SetValue(currentObj.GetComponent<Renderer>().material, i);

                // Set the material of the object hit to the hidermaterial
                currentObj.GetComponent<Renderer>().material = HiderMaterial;
            }
        }
    }
}
