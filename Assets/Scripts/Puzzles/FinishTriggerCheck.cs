/// <summary>
/// This script checks if the block GameObject has been pushed into the trigger. 
/// Once it's in the trigger the wall that blocks the player's progression
/// will be lowered.
/// </summary>

using UnityEngine;
using UnityEngine.AI;

public class FinishTriggerCheck : MonoBehaviour {
    private BossLidController m_BossLidController;

    [SerializeField] private int m_BossButtonIndex;

    //[SerializeField] private BossButton m_BossButton;
    //Put the PuzzleCube prefab in this field in the inspector
    [SerializeField]
    private GameObject block;

    private GameObject blockChild;
    //The wall GameObject is the wall behind the puzzle that blocks the player from
    //continuing without having finished the puzzle.
    [SerializeField]
    private GameObject wall;
    //speed decides how fast the wall travel downwards once the puzzle is finished
    [SerializeField]
    private float speed;
    //PuzzleIsFinished is used to tell when the wall should move downwards once the puzzle is finished
    private bool PuzzleIsFinished;
    private Vector3 m_TargetPos;

    private float m_OpenDoorTime = 5;
    private float m_CurrentOpenDoorTime;
    private float m_FadeInTime = 2f;
    private float m_CurrentFadeInTime;

    private Vector3 m_DoorStartPos;
    private Vector3 m_DoorTargetPos;

    private AudioSource m_AudioSource;
    private AudioClip m_AudioClip;

    private Color m_StartColor;
    private Color m_TargetColor;

    void Awake()
    {
        PuzzleIsFinished = false;
        m_TargetPos = wall.transform.position;
        m_TargetPos.y -= 2;
    }

    private void Start() {
        m_DoorStartPos = wall.transform.position;
        m_DoorTargetPos = m_DoorStartPos;
        m_DoorTargetPos.y -= 2;

        m_AudioClip = Resources.Load("Sound/SoundEffects/Long Impacts/PuzzleFinished_01") as AudioClip;
        m_AudioSource = GetComponent<AudioSource>();

        blockChild = wall.transform.FindChild("Edge").gameObject;

        m_StartColor = blockChild.GetComponent<Renderer>().material.color;
        m_TargetColor = Color.cyan;

        m_BossLidController = GameObject.FindGameObjectWithTag("BossLid").GetComponent<BossLidController>();
    }

    //When the puzzle cube is in the trigger the PuzzleIsFinished is switched to true 
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoxCollider>() == block.GetComponent<BoxCollider>()) {
            block.GetComponent<CubeChangeColor>().ChangeColor();
            block.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(block.GetComponent<NavMeshAgent>());
            block.AddComponent<NavMeshObstacle>();
            //PuzzleIsFinished = true;
            InvokeRepeating("OpenDoor", 0, 0.01f);
            //Destroy(other.GetComponent<Rigidbody>());
            GameController.Instance.PuzzlesSolved++;
            PuzzleGUIController.Instance.UpdatePuzzleGUIText();
            GetComponent<BoxCollider>().enabled = false;
            m_AudioSource.PlayOneShot(m_AudioClip);

//            if (m_BossButton != null) {
//                m_BossButton.Unlock();
//            }

            // Unlock the boss button
            if (m_BossLidController != null) {
                m_BossLidController.m_BossButtons[m_BossButtonIndex].Unlock();
            }

            InvokeRepeating("FadeInBlockadeColor", 0, 0.01f);
        }
    }

    private void FadeInBlockadeColor() {
        m_CurrentFadeInTime += Time.deltaTime;
        var t = m_CurrentFadeInTime / m_FadeInTime;

        blockChild.GetComponent<Renderer>().material.color = Color.Lerp(m_StartColor, m_TargetColor, t);

        if (m_CurrentFadeInTime > m_FadeInTime) {
            CancelInvoke("FadeInBlockadeColor");
        }
    }

    private void OpenDoor() {
        m_CurrentOpenDoorTime += Time.deltaTime;

        var t = m_CurrentOpenDoorTime / m_OpenDoorTime;

        wall.transform.position = Vector3.Lerp(m_DoorStartPos, m_DoorTargetPos, t);

        if (m_CurrentOpenDoorTime > m_OpenDoorTime) {
            CancelInvoke("OpenDoor");
        }
    }
//
//    void Update()
//    {
//        if (PuzzleIsFinished == true)
//        {
//            //Move the PuzzleLid GameObject downwards
//            //(the directions are for some reason scued and needs X to be -15 and Z to be 60 to let the Y axis move straight up/down)
//            wall.transform.position = Vector3.Lerp(wall.transform.position, m_TargetPos, Time.deltaTime * speed);
//        }
//    }
}

