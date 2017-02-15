using System;
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy {

    public EnemyStates_t EnemyState;

    public float HealthPoints { get; private set; }

    [SerializeField] private float m_HealthMax;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_AttackRange;
    [SerializeField] private float m_AggroRange;
    private GameObject m_ClosestPlayer;

    private GameObject[] m_PlayerTransforms;

    private NavMeshAgent m_EnemyAgent;

    // Use this for initialization
    private void Start() {
        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        EnemyState = EnemyStates_t.IDLE;
        HealthPoints = m_HealthMax;
        m_EnemyAgent = GetComponent<NavMeshAgent>();

        m_PlayerTransforms = new GameObject[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++) {
            m_PlayerTransforms[i] = allPlayers[i];
        }
    }

    // Update is called once per frame
    private void Update() {

        m_ClosestPlayer = m_PlayerTransforms[0];

        for (var i = 1; i < m_PlayerTransforms.Length; i++) {
            if (Vector3.Distance(m_PlayerTransforms[i].transform.position, transform.position) <
                Vector3.Distance(m_ClosestPlayer.transform.position, transform.position)) {
                m_ClosestPlayer = m_PlayerTransforms[i];
            }
        }
        CheckState();

        switch (EnemyState) {
            case EnemyStates_t.IDLE:
                break;
            case EnemyStates_t.CHASE:
                m_EnemyAgent.destination = m_ClosestPlayer.transform.position;
                break;
            case EnemyStates_t.ATTACK:
                break;
            case EnemyStates_t.DEAD:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CheckState() {
        switch (EnemyState) {
            case EnemyStates_t.IDLE:
                if (InAggroRange()) {
                    EnemyState = EnemyStates_t.CHASE;
                }
            break;
            case EnemyStates_t.CHASE:
                if (InAttackRange()) {
                    EnemyState = EnemyStates_t.ATTACK;
                } else if (!InAggroRange()) {
                    EnemyState = EnemyStates_t.IDLE;
                }
            break;
            case EnemyStates_t.ATTACK:
                if (!InAttackRange()) {
                    EnemyState = EnemyStates_t.CHASE;
                }
            break;
            case EnemyStates_t.DEAD:
            break;
            default:
                Debug.Log("No state");
            break;
        }
    }

    private bool InAttackRange() {
        return Vector3.Distance(transform.position, m_ClosestPlayer.transform.position) < m_AttackRange;
    }

    private bool InAggroRange() {
        return Vector3.Distance(transform.position, m_ClosestPlayer.transform.position) < m_AggroRange;
    }

    public void TakeDamage(int amount) {
        HealthPoints -= amount;
    }
}
