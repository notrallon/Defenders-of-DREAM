using UnityEngine;

public class CoopCamera : MonoBehaviour {
    public float OffsetY = 2.0f;
    public float MinDistance = 7.5f;

    [SerializeField]
    private float m_CameraMaxDistance = 20.0f;
    [SerializeField]
    private float m_CameraDistanceDivider = 1.5f;
    [SerializeField]
    private float m_CameraSmoothing = 10.0f;
    
    private float m_MinX;
    private float m_MaxX;
    private float m_MinZ;
    private float m_MaxZ;

    private void Start() {
        GameController.Instance.UpdatePlayers();
    }

    private void LateUpdate() {
        var allPlayers = GameController.Instance.PlayerInstances;
        if (allPlayers == null ||
            allPlayers.Length == 0) {
            return;
        }
        m_MinX = m_MaxX = allPlayers[0].position.x;
        m_MinZ = m_MaxZ = allPlayers[0].position.z;

        for (var i = 1; i < allPlayers.Length; i++) {
            if (allPlayers[i].position.x < m_MinX) {
                m_MinX = allPlayers[i].position.x;
            } else if (allPlayers[i].position.x > m_MaxX) {
                m_MaxX = allPlayers[i].position.x;
            }

            if (allPlayers[i].position.z < m_MinZ) {
                m_MinZ = allPlayers[i].position.z;
            } else if (allPlayers[i].position.z > m_MaxZ) {
                m_MaxZ = allPlayers[i].position.z;
            }
        }

        var xMiddle = (m_MinX + m_MaxX) / 2.0f;
        var zMiddle = (m_MinZ + m_MaxZ) / 2.0f;
        var distance = Mathf.Max(m_MaxX - m_MinX, m_MaxZ - m_MinZ);

        if (distance < MinDistance) {
            distance = MinDistance;
        } else if (distance > m_CameraMaxDistance) {
            distance = m_CameraMaxDistance;
        }
        
        distance /= m_CameraDistanceDivider;

        Vector3 targetCamPos = new Vector3(xMiddle, distance, zMiddle - distance);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, m_CameraSmoothing * Time.deltaTime);
    }
}
