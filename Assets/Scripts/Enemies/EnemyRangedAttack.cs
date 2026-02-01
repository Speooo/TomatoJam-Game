using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioClip shootAudio;

    private EnemyController controller;
    private MaskHolder playerMaskHolder;

    private float attackTimer;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Start()
    {
        playerMaskHolder = controller.Player.GetComponent<MaskHolder>();
    }

    public void HandleRangedAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0)
            return;

        if (Vector3.Distance(transform.position, controller.Player.position) <= attackRange)
        {
            attackTimer = attackCooldown;
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            projectile.GetComponent<EnemyProjectile>().Initialise(shootPoint.position, controller.Player, playerMaskHolder);
            AudioManager.Instance.PlaySfx3D(shootAudio, shootPoint.position, 1f);
        }
    }
}
