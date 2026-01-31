using UnityEngine;

public class EnemyAreaEnterTrigger : MonoBehaviour
{
    [SerializeField] private int enemyIndex;

    public event System.Action<int> OnPlayerEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterTrigger.Invoke(enemyIndex);
        }
    }
}
