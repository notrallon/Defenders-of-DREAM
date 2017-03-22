using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour, IEnemy
{

    public EnemyStates_t EnemyState;

    public float HealthPoints { get; private set; }

    private ParticleController particleController;

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
    [SerializeField]
    float damage = 10;
    private float NextFire;
    [SerializeField]
    protected float FireRate = 1.5f;
    GameObject playerObject;

    public float turnRate = 2;

    private GameObject[] m_PlayerTransforms;

    private NavMeshAgent m_EnemyAgent;
    private EnemyManager m_EnemyManager;

    private float m_SinkTime = 1f;
    private float m_CurrentSinkTime;

    private Vector3 m_StartSinkPos;
    private Vector3 m_TargetSinkPos;

    // Flash when damaged
    //[SerializeField]
    private float flashLength = 0.3f;   // set time length
    private float flashCounter; // countdown timer

    private Renderer rend; // this will render the flash
    private Color storedColor; // store current color

    private Color m_LastPlayerHitColor;

    // Use this for initialization
    protected virtual void Start()
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

        //particleController = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<ParticleController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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
                m_EnemyAgent.destination = transform.position;
                break;
            case EnemyStates_t.CHASE:
                m_EnemyAgent.destination = m_ClosestPlayer.transform.position;
                break;
            case EnemyStates_t.ATTACK:
                m_EnemyAgent.destination = transform.position;
                Vector3 dir = (m_ClosestPlayer.position - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnRate * Time.deltaTime);
                Attack();
                break;
            case EnemyStates_t.DEAD://NOT in use
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

    public void Sink() {
        m_StartSinkPos = transform.position;
        m_TargetSinkPos = m_StartSinkPos;
        m_TargetSinkPos.y -= 2f;

        InvokeRepeating("SinkAndDestroy", 0.1f, 0.01f);
    }

    private void SinkAndDestroy() {
        m_CurrentSinkTime += Time.deltaTime;

        var t = m_CurrentSinkTime / m_SinkTime;

        transform.position = Vector3.Lerp(m_StartSinkPos, m_TargetSinkPos, t);

        if (m_CurrentSinkTime > m_SinkTime) {
            Destroy(gameObject);
        }
    }

    private void Die() {
        if (m_EnemyManager != null) {
            m_EnemyManager.EnemiesAlive--;
        }
        //particleController.EmitSplatterAtLocation(transform, m_LastPlayerHitColor);
        ScoreTracker.score += 5;
        if (GetComponent<Animator>() != null) {
            int i = Random.Range(0, 3);

            EnemyState = EnemyStates_t.DEAD;

            GetComponent<Animator>().SetTrigger("Death" + i);
            Destroy(m_EnemyAgent);
        } else {
            Destroy(gameObject);
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
    public void TakeDamage(float amount, Color color) {
        if (EnemyState == EnemyStates_t.DEAD) {
            return;
        }
        HealthPoints -= amount;
        //particleController.EmitSplatterAtLocation(transform, m_LastPlayerHitColor);
        if (HealthPoints < 1) {
            m_LastPlayerHitColor = color;
            Die();
        }

        flashCounter = flashLength; // count down timer is set
        rend.material.SetColor("_Color", Color.red); // set material color to red
    }

    //take damage
    public void TakeDamage(float amount) {
        HealthPoints -= amount;
        //particleController.EmitSplatterAtLocation(transform, m_LastPlayerHitColor);
        if (HealthPoints < 1) {
            m_LastPlayerHitColor = Color.red;
            Die();
        }

        flashCounter = flashLength; // count down timer is set
        rend.material.SetColor("_Color", Color.red); // set material color to red
    }

    public void SetEnemyManagerIndex(EnemyManager manager) {
        m_EnemyManager = manager;
    }

    public virtual void Attack()
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
