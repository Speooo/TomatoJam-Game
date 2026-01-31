using System;
using UnityEngine;

public class ActiveMask : MonoBehaviour
{
    [SerializeField] private GameObject maskProtoype;
    [SerializeField] private MaskHolder playerMaskHolder;

    public static ActiveMask Instance;

    public event Action<Mask> OnEnemyDied;

    public Mask currentMask { get; private set; }
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

    public void GiveMask(Mask mask, MaskHolder newHolder)
    {
        if (CurrentMaskHolder != null)
            CurrentMaskHolder.OnMaskLost();
        else
            CurrentMaskHolder = playerMaskHolder;

        CurrentMaskChaser = CurrentMaskHolder;
        currentMask = mask;
        CurrentMaskHolder = newHolder;

        CurrentMaskHolder.OnMaskGained(mask);
    }

    public void GiveMaskPermanentlyToPlayer()
    {
        if (currentMask != null)
            currentMask.permanentlyOwned = true;

        OnEnemyDied?.Invoke(currentMask);

        currentMask = null;
    }
    
    public void BeginNewEnemyCombat(MaskHolder holder)
    {
        Mask newMask = Instantiate(maskProtoype, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Mask>();
        currentMask = newMask;

        GiveMask(currentMask, holder);
    }
}
