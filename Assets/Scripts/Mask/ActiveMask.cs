using System;
using UnityEngine;

public class ActiveMask : MonoBehaviour
{
    [SerializeField] private MaskHolder playerMaskHolder;

    public static ActiveMask Instance;

    public event Action OnEnemyDied;

    public MaskHolder CurrentMaskHolder { get; private set; }
    public MaskHolder CurrentMaskChaser { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool AmIHoldingMask(MaskHolder holder)
    {
        return CurrentMaskHolder == holder;
    }

    public void GiveMask(MaskHolder newHolder)
    {
        if (CurrentMaskHolder != null)
            CurrentMaskHolder.OnMaskLost();
        else
            CurrentMaskHolder = playerMaskHolder;

        CurrentMaskChaser = CurrentMaskHolder;
        CurrentMaskHolder = newHolder;

        CurrentMaskHolder.OnMaskGained();
    }

    public void GiveMaskPermanentlyToPlayer()
    {
        OnEnemyDied?.Invoke();
    }
    
    public void BeginNewEnemyCombat(MaskHolder holder)
    {
        GiveMask(holder);
    }
}
