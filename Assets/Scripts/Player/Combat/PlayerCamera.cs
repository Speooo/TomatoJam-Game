using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerArm;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector3 cameraCenter;
    [SerializeField] private Vector3 cameraExtents;
    [SerializeField] private float cameraCooldown;

    private float cameraTimer;

    private void OnEnable()
    {
        playerArm.SetActive(true);
    }

    private void OnDisable()
    {
        playerArm.SetActive(false);
    }

    public void HandleCamera(bool attack)
    {
        cameraTimer -= Time.deltaTime;
        if (cameraTimer > 0 || !this.isActiveAndEnabled)
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
