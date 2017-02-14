using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy {

    public EnemyStates_t EnemyState;

    public float HealthPoints { get; private set; }

    [SerializeField] private float m_HealthMax;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_AttackRange;
    [SerializeField] private float m_AggroRange;
    [SerializeField] private GameObject m_PlayerGameObject;

    private NavMeshAgent m_EnemyAgent;

    // Use this for initialization
    private void Start() {
        EnemyState = EnemyStates_t.IDLE;
        HealthPoints = m_HealthMax;
        m_EnemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update() {
        CheckState();

        switch (EnemyState) {
            case EnemyStates_t.IDLE:
                break;
            case EnemyStates_t.CHASE:
                m_EnemyAgent.destination = m_PlayerGameObject.transform.position;
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
        return Vector3.Distance(transform.position, m_PlayerGameObject.transform.position) < m_AttackRange;
    }

    private bool InAggroRange() {
        return Vector3.Distance(transform.position, m_PlayerGameObject.transform.position) < m_AggroRange;
    }

    public void TakeDamage(int amount) {
        HealthPoints -= amount;
    }
}
