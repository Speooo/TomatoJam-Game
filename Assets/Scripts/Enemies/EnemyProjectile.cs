using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 direction;
    private MaskHolder playerMaskHolder;
    private PlayerHealth playerHealth;

    public void Initialise(Vector3 shootPoint, Transform player, MaskHolder playerMaskHolder)
    {
        this.playerMaskHolder = playerMaskHolder;
        playerHealth = playerMaskHolder.transform.GetComponent<PlayerHealth>();

        RaycastHit hitInfo;

        Physics.Raycast(shootPoint, ((player.position + Vector3.up) - shootPoint).normalized, out hitInfo, Mathf.Infinity);
        direction = (hitInfo.point - shootPoint).normalized;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Vector3 velocity = direction * speed * Time.deltaTime;

        if (Physics.SphereCast(transform.position, 0.5f, velocity.normalized, out RaycastHit hit, velocity.magnitude))
        {
            if (hit.transform.CompareTag("Player"))
            {
                if (playerMaskHolder.holdingMask)
                {
                    playerMaskHolder.TakeDamage();
                }
                else
                {
                    playerHealth.ReceiveHealthDamage(15f);
                }

                Debug.Log("projectile landed on player");
            }
            
            Destroy(gameObject);
        }
        else
            transform.position += velocity;
    }
}
