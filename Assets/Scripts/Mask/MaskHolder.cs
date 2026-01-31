using UnityEngine;

public class MaskHolder : MonoBehaviour
{
    private Mask heldMask;
    private bool isPlayer;

    private void Start()
    {
        isPlayer = TryGetComponent<PlayerController>(out _);
    }
    
    public void TakeDamage()
    {
        Debug.Log(transform.name + " took damage");
        if (ActiveMask.Instance.AmIHoldingMask(this))
            DropMask();
        else
            Die();
    }

    private void DropMask()
    {
        ActiveMask.Instance.GiveMask(heldMask, ActiveMask.Instance.CurrentMaskChaser);
    }

    private void Die()
    {
        //if (!isPlayer)
        //    ActiveMask.Instance.GiveMaskPermanentlyToPlayer();
        // triger checkpoint reset
    }

    public void OnMaskLost()
    {
        heldMask = null;

        // enter aggro state
    }

    public void OnMaskGained(Mask mask)
    {
        heldMask = mask;

        // do mask visuals
    }
}
