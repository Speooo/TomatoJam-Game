using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float memoryTimeMax;
    [SerializeField] private float lifeForceMax;

    public Transform Player => player;
    public EnemySensors Sensors => sensors;
    public bool IsActive => isActive;

    private Transform player;
    private EnemyMotor motor;
    private EnemySensors sensors;
    private EnemyMelee melee;
    private EnemyRangedAttack ranged;
    private NavMeshAgent agent;

    private bool isActive = false;
    private bool isDynamic = false;
    private float memoryTimer;
    private float lifeForce;

    private EnemyState enemyState = EnemyState.None;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isDynamic = TryGetComponent<EnemyMotor>(out motor);
        sensors = GetComponent<EnemySensors>();
        melee = GetComponent<EnemyMelee>();
        ranged = GetComponent<EnemyRangedAttack>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = true;
        lifeForce = lifeForceMax;
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (!isDynamic)
        {
            ranged.HandleRangedAttack();
            return;
        }

        HandleContext();

        switch (enemyState)
        {
            case EnemyState.None:

                break;
            case EnemyState.Passive:

                motor.HandlePatrol();

                break;
            case EnemyState.Chase:

                motor.HandleChase(player);
                melee.HandleMelee();

                if (ranged != null)
                    ranged.HandleRangedAttack();

                break;
            case EnemyState.Aggro:

                motor.HandleChase(player);
                melee.HandleMelee();

                if (ranged != null)
                    ranged.HandleRangedAttack();

                break;
        }
    }

    private void HandleContext()
    {
        EnemyContext context = sensors.BuildEnemyContext(player);

        if (!context.hasLOS)
            memoryTimer -= Time.deltaTime;
        else
            memoryTimer = memoryTimeMax;


        switch (enemyState)
        {
            case EnemyState.Passive:

                if (context.hasLOS)
                {
                    memoryTimer = memoryTimeMax;
                    SwitchState(EnemyState.Chase);
                }

                break;
            case EnemyState.Chase:

                if (!context.hasLOS && memoryTimer < 0f)
                    SwitchState(EnemyState.Passive);

                break;
            default:
                break;
        }
    }
    [ContextMenu("Initialise")]
    public void InitialiseEnemy()
    {
        isActive = true;
        SwitchState(EnemyState.Passive);
        //SnapAgentToNavMesh(agent);
    }

    public void EnemyMaskOff()
    {
        SwitchState(EnemyState.Aggro);
    }

    public bool LifeForceDepleted()
    {
        lifeForce -= Time.deltaTime;
        return lifeForce <= 0f;
    }

    public void SwitchState(EnemyState state)
    {
        if (enemyState == EnemyState.Aggro)
            return;

        if (enemyState == state)
            return;

        enemyState = state;
    }

    public static void SnapAgentToNavMesh(NavMeshAgent agent)
    {
        NavMeshHit hit;
        // Tries to find nearest valid position within 1 unit
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position); // instantly moves agent to valid spot
        }
        else
        {
            Debug.LogWarning("No valid navmesh position found near spawn.");
        }
    }
}

public enum EnemyState
{
    None,
    Passive,
    Chase,
    Aggro
}
