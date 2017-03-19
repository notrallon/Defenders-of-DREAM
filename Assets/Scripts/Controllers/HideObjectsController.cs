using System.Collections.Generic;
using UnityEngine;

public class HideObjectsController  {

    public static HideObjectsController Instance { get {
        return m_Instance ?? (m_Instance = new HideObjectsController());
    } }

    private static HideObjectsController m_Instance;

    public Dictionary<Transform, Material[]> ObjectsToHide = new Dictionary<Transform, Material[]>();

    public void Update() {
        if (ObjectsToHide.Count > 0) {
            foreach (Transform t in ObjectsToHide.Keys) {
                t.GetComponent<Renderer>().materials = ObjectsToHide[t];
            }
            ObjectsToHide.Clear();
        }
    }
}
