using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;
    [SerializeField] private float castRadius;

    private Vector3 direction;
    private MaskHolder playerMaskHolder;

    public void Initialise(Vector3 shootPoint, Transform player, MaskHolder playerMaskHolder)
    {
        this.playerMaskHolder = playerMaskHolder;

        RaycastHit hitInfo;

        Physics.Raycast(shootPoint, ((player.position + Vector3.up) - shootPoint).normalized, out hitInfo, Mathf.Infinity);
        direction = (hitInfo.point - shootPoint).normalized;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Vector3 velocity = direction * speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, velocity, out RaycastHit hit, 0.1f, playerLayer))
        {
            playerMaskHolder.TakeDamage();
            Debug.Log("projectile landed on player");
        }
        else
            transform.position += velocity;
    }
}
