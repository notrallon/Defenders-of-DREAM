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
    private Transform m_ClosestPlayer;

    //damage variables
    float damage = 10;
    private float NextFire;
    float FireRate = 1.5f;
    GameObject playerObject;


    private GameObject[] m_PlayerTransforms;

    private NavMeshAgent m_EnemyAgent;

    // Flash when damaged
    public float flashLength;   // set time length
    private float flashCounter; // countdown timer

    private Renderer rend; // this will render the flash
    private Color storedColor; // store current color


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

        rend = GetComponentInChildren<Renderer>(); // get renderer of first child
        storedColor = rend.material.GetColor("_Color");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

//        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
//
//        if (allPlayers.Length == 0) return;
//
//        m_PlayerTransforms = new GameObject[allPlayers.Length];
//
//        for (var i = 0; i < allPlayers.Length; i++)
//        {
//            m_PlayerTransforms[i] = allPlayers[i];
//        }

        var allPlayers = GameController.Instance.PlayerInstances;
        if (allPlayers == null ||
            allPlayers.Length == 0) {
            m_ClosestPlayer = null;
            return;
        }

        m_ClosestPlayer = GameController.Instance.PlayerInstances[0];

        for (var i = 1; i < allPlayers.Length; i++) {
            if (Vector3.Distance(allPlayers[i].position, transform.position) <
                Vector3.Distance(m_ClosestPlayer.position, transform.position)) {
                m_ClosestPlayer = allPlayers[i];
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

        // Flash when damaged
        if (flashCounter > 0) // if flashcounter is more than 0
        {
            flashCounter -= Time.deltaTime; // start counting down
            if (flashCounter <= 0)  // when count down is finished
            {
                rend.material.SetColor("_Color", storedColor); // reset the color to original
            }
        }

        if (HealthPoints < 1) {
            ScoreTracker.score += 5;
            Destroy(gameObject);
        }
    }

    private void CheckState()
    {
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

        if (GameController.Instance.PlayerInstances.Length < 1) {
            EnemyState = EnemyStates_t.IDLE;
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

        flashCounter = flashLength; // count down timer is set
        rend.material.SetColor("_Color", Color.red); // set material color to red
    }

    public void Attack()
    {
        if (!(NextFire <= Time.time)) return;
        playerObject = m_ClosestPlayer.gameObject;
        var script = playerObject.GetComponent<PlayerHealth>();
        script.TakeDamage(damage);

        NextFire = Time.time + FireRate;

        //Debug.Log("Attacked once every 1,5s");
    }

    //damage player
    private void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        playerObject = col.gameObject;
        var script = playerObject.GetComponent<PlayerHealth>();
        script.TakeDamage(damage);

        NextFire = Time.deltaTime + FireRate;

        //Debug.Log("Attacked once every 1s");
    }
}
