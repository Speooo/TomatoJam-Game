using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private float patrolMinRange;
    [SerializeField] private float patrolMaxRange;
    [SerializeField] private float eyeHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private int maxAttempts = 20;
    private float rayHeight = 5f;
    private float rayLength = 20f;

    private float chaseRepathTimer;
    private float chaseRepathMaxTime = 0.5f;

    private float patrolTimer;
    private bool hasPatrolPoint;
    private bool hasArrived;
    private Vector3 currentPatrolPoint;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
    }

    private void Start()
    {
        hasArrived = true;
        patrolTimer = Random.Range(0f, 2f);
    }

    public void HandlePatrol()
    {
        if (hasPatrolPoint)
        {
            agent.SetDestination(currentPatrolPoint);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                hasArrived = true;
                hasPatrolPoint = false;
            }
        }
        else if (hasArrived)
        {
            // countdown timer
            patrolTimer -= Time.deltaTime;

            if (patrolTimer > 0f)
                return;

            currentPatrolPoint = GetNewPatrolPoint();
            hasPatrolPoint = true;
            hasArrived = false;
            patrolTimer = Random.Range(0.1f, 1f);
        }
        else
        {
            Debug.Log("enemy's patrol got bugged out");
            hasArrived = true;
        }
    }

    private Vector3 GetNewPatrolPoint()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;
            float distance = Random.Range(patrolMinRange, patrolMaxRange);

            Vector3 candidate = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y) * distance;

            Vector3 rayOrigin = candidate + Vector3.up * rayHeight;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength, groundLayer))
            {
                //if (!Physics.Linecast(transform.position + Vector3.up * eyeHeight, hit.point + Vector3.up * skin, obstacleLayer))
                //{
                //    return hit.point;
                //}
                return hit.point;
            }
        }

        return transform.position;
    }

    public void HandleChase(Transform player)
    {
        chaseRepathTimer -= Time.deltaTime;

        if (chaseRepathTimer > 0f)
            return;

        chaseRepathTimer = chaseRepathMaxTime;

        agent.SetDestination(player.position);
    }
}
