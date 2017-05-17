using UnityEngine;

/*
===============================================================================

	CoopCamera loops through all players that are currently in the game and
    tries to center it's view around them.

===============================================================================
*/
public class CoopCamera : MonoBehaviour {
    public float OffsetY = 2.0f;
    public float MinDistance = 7.5f;

    // Values that can be changed in the inspector.
    [SerializeField]
    private float m_CameraMaxDistance = 20.0f;
    [SerializeField]
    private float m_CameraDistanceDivider = 1.5f;
    [SerializeField]
    private float m_CameraSmoothing = 10.0f;
    
    // These values are updated each frame and used to center the camera.
    private float m_MinX;
    private float m_MaxX;
    private float m_MinZ;
    private float m_MaxZ;
    private float m_Distance;



    private void Start() {
        // Do an update to see how many player instances that are in the game to avoid errors.
        GameController.Instance.UpdatePlayers();
        GameController.Instance.FindAllPuzzles();
        Debug.Log(GameController.Instance.PuzzlesTotal);



        if (GetComponent<CameraShaker>() == null) {
            gameObject.AddComponent<CameraShaker>();
        }
    }

    private void Update() {
        HideObjectsController.Instance.Update();
    }

    private void LateUpdate() {
        var allPlayers = GameController.Instance.PlayerInstances; // Get the reference to all player instances

        // If there's no players in the game, exit the function.
        if (allPlayers == null ||
            allPlayers.Length == 0) {
            return;
        }

        // Set min & max values to the first player instance
        m_MinX = m_MaxX = allPlayers[0].position.x;
        m_MinZ = m_MaxZ = allPlayers[0].position.z;

        // Loop through all players and check if min and max values needs to be changed.
        for (var i = 1; i < allPlayers.Length; i++) {
            // If current player in the loop is further to the left
            if (allPlayers[i].position.x < m_MinX) {
                m_MinX = allPlayers[i].position.x; // Set minX value to be at the current players X position

                // If the current player in the loop is further to the right
            } else if (allPlayers[i].position.x > m_MaxX) {
                m_MaxX = allPlayers[i].position.x; // Set maxX value to be at the current players X position
            }

            // Same as X but for Z
            if (allPlayers[i].position.z < m_MinZ) {
                m_MinZ = allPlayers[i].position.z;
            } else if (allPlayers[i].position.z > m_MaxZ) {
                m_MaxZ = allPlayers[i].position.z;
            }
        }

        // Find the middle point of the min and max values.
        var xMiddle = (m_MinX + m_MaxX) / 2.0f;
        var zMiddle = (m_MinZ + m_MaxZ) / 2.0f;

        var distX = m_MaxX - m_MinX;
        var distZ = m_MaxZ - m_MinZ;

        distZ /= 9;
        distZ *= 16;

        // Check if X or Z distance is the largest.
        m_Distance = Mathf.Max(m_MaxX - m_MinX, distZ);

        // Clamp the distance so that it can't get lower/higher than our min/max distance
        m_Distance = Mathf.Clamp(m_Distance, MinDistance, m_CameraMaxDistance);
        /*
        if (distance < MinDistance) {
            distance = MinDistance;
        } else if (distance > m_CameraMaxDistance) {
            distance = m_CameraMaxDistance;
        }*/

        // Calculate fog density depending on distance.
        const int fog = 30;
        RenderSettings.fogDensity = (fog - m_Distance) * 0.003f;
        var clipPlane = m_Distance * 2;
        clipPlane = Mathf.Max(clipPlane, 30);
        Camera.main.farClipPlane = clipPlane;
        m_Distance /= m_CameraDistanceDivider;

        // Set the target camera position and tween to get a smooth motion.
        var targetCamPos = new Vector3(xMiddle, m_Distance, zMiddle - m_Distance);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, m_CameraSmoothing * Time.deltaTime);
    }
}
