using UnityEngine;

public class EnemySensors : MonoBehaviour
{
    [SerializeField] private float angleView;

    private Vector3 lastSeenPos;

    public EnemyContext BuildEnemyContext(Transform target)
    {
        bool los = true;

        Vector3 toPlayer = target.position - transform.position;
        float distance = toPlayer.magnitude + 2f; // + fallback in case it misses by a bit

        Vector3 dir = toPlayer.normalized;

        float halfAngle = angleView * 0.5f;
        float angleToTarget = Vector3.Angle(transform.forward, dir);

        if (angleToTarget > halfAngle)
            los = false;

        if (Physics.Raycast(transform.position + Vector3.up, toPlayer.normalized, out RaycastHit hitInfo, distance))
        {
            if (!hitInfo.transform.CompareTag("Player"))
                los = false;
        }

        lastSeenPos = target.position;

        EnemyContext ctx = new EnemyContext
        {
            hasLOS = los,
            lastSeenPosition = lastSeenPos,
        };

        return ctx;
    }
}

public struct EnemyContext
{
    public bool hasLOS;
    public Vector3 lastSeenPosition;
}
