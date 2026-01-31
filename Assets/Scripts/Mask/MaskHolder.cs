using UnityEngine;

public class MaskHolder : MonoBehaviour
{
    private bool holdingMask;
    private bool isEnemy;
    private EnemyController enemy;

    private void Start()
    {
        isEnemy = TryGetComponent<EnemyController>(out enemy);
    }

    private void Update()
    {
        if (!isEnemy)
        {
            return;
        }

        if (!holdingMask && enemy.IsActive)
        {
            if (enemy.LifeForceDepleted())
                Die();
        }
    }

    public void TakeDamage()
    {
        if (ActiveMask.Instance.AmIHoldingMask(this))
            ExchangeMask();
    }

    private void ExchangeMask()
    {
        ActiveMask.Instance.GiveMask(ActiveMask.Instance.CurrentMaskChaser);
    }

    private void Die()
    {
        if (isEnemy && !holdingMask)
        {
            ActiveMask.Instance.GiveMaskPermanentlyToPlayer();
            Destroy(enemy.gameObject);
        }
    }

    public void OnMaskLost()
    {
        holdingMask = false;

        if (isEnemy)
            enemy.SwitchState(EnemyState.Aggro);
    }

    public void OnMaskGained()
    {
        holdingMask = true;
    }
}
