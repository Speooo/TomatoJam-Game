using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private float meleeDistance;
    [SerializeField] private float meleeCooldown;

    private EnemyController controller;
    private MaskHolder playerMaskHolder;

    private float meleeTimer;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Start()
    {
        playerMaskHolder = controller.Player.GetComponent<MaskHolder>();
    }

    public void HandleMelee()
    {
        meleeTimer -= Time.deltaTime;
        if (meleeTimer > 0)
            return;
        meleeTimer = meleeCooldown;

        if (Vector3.Distance(transform.position, controller.Player.position) < meleeDistance)
        {
            playerMaskHolder.TakeDamage();
            Debug.Log("Dealt damage to player");
        }
    }
}
