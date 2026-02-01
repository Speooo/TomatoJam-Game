using UnityEngine;

public class EnemyAreaEnterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private AudioClip growl;

    public event System.Action<GameObject, Vector3> OnPlayerEnterTrigger;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            OnPlayerEnterTrigger.Invoke(targetPrefab, enemySpawnPoint.position);
            AudioManager.Instance.PlaySfx2D(growl, 1f);
            hasTriggered = true;
        }
    }
}
