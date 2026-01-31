using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector3 cameraCenter;
    [SerializeField] private Vector3 cameraExtents;
    [SerializeField] private float cameraCooldown;

    private float cameraTimer;

    public void HandleCamera(bool attack)
    {
        cameraTimer -= Time.deltaTime;
        if (cameraTimer > 0)
            return;

        if (attack)
        {
            Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(cameraCenter), cameraExtents * 0.5f, transform.rotation, enemyLayer);

            foreach (Collider collider in colliders)
            {
                collider.GetComponent<MaskHolder>().TakeDamage();
            }
            Debug.Log("Flashed camera");
            cameraTimer = cameraCooldown;
        }
    }
}
