using System;
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy
{

    public EnemyStates_t EnemyState;

    public float HealthPoints { get; private set; }

    [SerializeField]
    private float m_HealthMax;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_AttackRange = 500;
    [SerializeField]
    private float m_AggroRange;
    private GameObject m_ClosestPlayer;

    //damage variables
    float damage = 10;
    private float NextFire;
    float FireRate = 1.5f;
    GameObject playerObject;


    private GameObject[] m_PlayerTransforms;

    private NavMeshAgent m_EnemyAgent;

    // Use this for initialization
    private void Start()
    {
        
        EnemyState = EnemyStates_t.IDLE;
        HealthPoints = m_HealthMax;
        m_EnemyAgent = GetComponent<NavMeshAgent>();

        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        m_PlayerTransforms = new GameObject[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++)
        {
            m_PlayerTransforms[i] = allPlayers[i];
        }
    }

    // Update is called once per frame
    private void Update()
    {

        var allPlayers = GameObject.FindGameObjectsWithTag("Player");

        if (allPlayers.Length == 0) return;

        m_PlayerTransforms = new GameObject[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++)
        {
            m_PlayerTransforms[i] = allPlayers[i];
        }

        m_ClosestPlayer = m_PlayerTransforms[0];

        for (var i = 1; i < m_PlayerTransforms.Length; i++)
        {
            if (Vector3.Distance(m_PlayerTransforms[i].transform.position, transform.position) <
                Vector3.Distance(m_ClosestPlayer.transform.position, transform.position))
            {
                m_ClosestPlayer = m_PlayerTransforms[i];
            }
        }
        CheckState();

        switch (EnemyState)
        {
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

    private void CheckState()
    {
        if (HealthPoints < 1)
        {
            Destroy(gameObject);
        }

        switch (EnemyState)
        {
            case EnemyStates_t.IDLE:
                if (InAggroRange())
                {
                    EnemyState = EnemyStates_t.CHASE;
                }
                break;
            case EnemyStates_t.CHASE:
                if (InAttackRange())
                {
                    EnemyState = EnemyStates_t.ATTACK;
                }
                else if (!InAggroRange())
                {
                    EnemyState = EnemyStates_t.IDLE;
                }
                break;
            case EnemyStates_t.ATTACK:
                if (!InAttackRange())
                {
                    EnemyState = EnemyStates_t.CHASE;
                }
                else
                {
                    Attack();
                }
                break;
            case EnemyStates_t.DEAD:
                break;
            default:
                Debug.Log("No state");
                break;
        }
    }

    private bool InAttackRange()
    {
        return Vector3.Distance(transform.position, m_ClosestPlayer.transform.position) < m_AttackRange;
    }

    private bool InAggroRange()
    {
        return Vector3.Distance(transform.position, m_ClosestPlayer.transform.position) < m_AggroRange;
    }

    //take damage
    public void TakeDamage(float amount)
    {
        HealthPoints -= amount;
    }

    public void Attack()
    {
        if (NextFire <= Time.time)
        {
        playerObject = m_ClosestPlayer;
        var script = playerObject.GetComponent<PlayerHealth>();
        script.TakeDamage(damage);

        NextFire = Time.time + FireRate;

        //Debug.Log("Attacked once every 1,5s");
        }
    }

    //damage player
    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Player"))
        {
            playerObject = col.gameObject;
            var script = playerObject.GetComponent<PlayerHealth>();
            script.TakeDamage(damage);

            NextFire = Time.deltaTime + FireRate;

            //Debug.Log("Attacked once every 1s");
        }

    }
}
