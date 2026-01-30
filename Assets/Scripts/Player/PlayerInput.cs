using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public FrameInput FrameInput;

    private InputActions input;
    private bool IsInputLocked;
    public void LockInput() => IsInputLocked = true;
    public void UnlockInput() => IsInputLocked = false;

    private void Awake()
    {
        input = new InputActions();
        input.Enable();
    }

    private FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = input.Player.Move.ReadValue<Vector2>(),
            Look = input.Player.Look.ReadValue<Vector2>(),
            Interact = input.Player.Interact.WasPressedThisFrame(),
            Jump = input.Player.Jump.WasPressedThisFrame(),
            Attack = input.Player.Attack.WasPressedThisFrame(),
        };
    }

    private void Update()
    {
        FrameInput = IsInputLocked ? default : GatherInput();
    }
}

public struct FrameInput
{
    public Vector2 Move;
    public Vector2 Look;
    public bool Interact;
    public bool Jump;
    public bool Attack;
}
