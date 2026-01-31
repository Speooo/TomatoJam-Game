using UnityEngine;

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

    private bool isActive = false;
    private float memoryTimer;
    private float lifeForce;

    private EnemyState enemyState = EnemyState.None;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        motor = GetComponent<EnemyMotor>();
        sensors = GetComponent<EnemySensors>();
        melee = GetComponent<EnemyMelee>();

        lifeForce = lifeForceMax;
    }

    private void Update()
    {
        if (!isActive)
            return;

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

                break;
            case EnemyState.Aggro:

                motor.HandleChase(player);
                melee.HandleMelee();

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
    
    public void InitialiseEnemy()
    {
        isActive = true;
        SwitchState(EnemyState.Passive);
    }

    public void EnemyMaskOff()
    {
        SwitchState(EnemyState.Aggro);
    }

    public bool LifeForceDepleted()
    {
        lifeForce -= Time.deltaTime;
        Debug.Log("depleting life force");
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
}

public enum EnemyState
{
    None,
    Passive,
    Chase,
    Aggro
}
