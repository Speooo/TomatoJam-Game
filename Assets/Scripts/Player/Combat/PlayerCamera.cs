using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerArm;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector3 cameraCenter;
    [SerializeField] private Vector3 cameraExtents;
    [SerializeField] private float cameraCooldown;
    [SerializeField] private AudioClip cameraFlash;
    [SerializeField] private GameObject flashLight;

    private float cameraTimer;
    private float flashDuration = 0.2f;

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
            StartCoroutine(PlayCameraFlash());

            Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(cameraCenter), cameraExtents * 0.5f, transform.rotation, enemyLayer);

            foreach (Collider collider in colliders)
            {
                collider.GetComponent<MaskHolder>().TakeDamage();
            }
            AudioManager.Instance.PlaySfx2D(cameraFlash, 0.25f);
            cameraTimer = cameraCooldown;
        }
    }

    private IEnumerator PlayCameraFlash()
    {
        flashLight.SetActive(true);
        float t = 0f;

        while (t < flashDuration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        flashLight.SetActive(false);
    }
}
