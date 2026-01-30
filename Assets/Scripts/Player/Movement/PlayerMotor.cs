using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float airAcceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityStrength;

    public Vector3 CurrentVelocity;

    public LayerMask GroundLayer => groundLayer;
    public float MovementSpeed => movementSpeed;
    public float AirAcceleration => airAcceleration;
    public float JumpForce => jumpForce;
    public float Gravity => gravityStrength;

    private FrameInput cachedInput;
    private IMovement curMoveMode = new PlayerGroundMovement();

    private void Start()
    {
        SetMoveState(new PlayerGroundMovement());
    }

    public void SetInput(FrameInput input)
    {
        cachedInput = input;
    }

    private void Update()
    {
        curMoveMode?.Tick(this, cachedInput, Time.deltaTime);
    }

    public void SetMoveState(IMovement move)
    {
        if (curMoveMode == move)
            return;

        curMoveMode?.Exit(this);
        curMoveMode = move;
        curMoveMode.Enter(this);
    }
}
