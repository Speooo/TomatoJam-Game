using UnityEngine;

public class PlayerGroundMovement : IMovement
{
    private float playerRadius = 0.5f;
    private float groundedDistanceCheck = 0.1f;
    private float groundSnapDistance = 5f;
    private float skin = 0.01f;

    private LayerMask ground;
    private float moveSpeed;
    private float jumpForce;

    private bool isGrounded;

    public void Enter(PlayerMotor motor)
    {
        ground = motor.GroundLayer;
        moveSpeed = motor.MovementSpeed;
        jumpForce = motor.JumpForce;

        SnapToGround(motor);
        motor.CurrentVelocity.y = 0f;
    }

    public void Tick(PlayerMotor motor, FrameInput input, float dt)
    {
        Vector3 moveDir = new Vector3(input.Move.x, 0f, input.Move.y);
        Vector3 desiredHorizontal = motor.transform.rotation * moveDir * moveSpeed;

        motor.CurrentVelocity.x = desiredHorizontal.x;
        motor.CurrentVelocity.z = desiredHorizontal.z;

        if (CheckGrounded(motor))
        {
            if (input.Jump)
            {
                motor.CurrentVelocity.y = jumpForce;
                motor.SetMoveState(new PlayerAirMovement());
            }
        }
        else
        {
            motor.CurrentVelocity += motor.Gravity * dt;
        }

        motor.transform.position += motor.CurrentVelocity * dt;
        isGrounded = CheckGrounded(motor);
    }

    public void Exit(PlayerMotor motor)
    {
        
    }

    private void SnapToGround(PlayerMotor motor)
    {
        Vector3 origin = motor.transform.position + Vector3.up;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hitInfo, groundSnapDistance, ground))
            motor.transform.position = hitInfo.point + Vector3.up * skin;
    }

    private bool CheckGrounded(PlayerMotor motor)
    {
        return Physics.SphereCast(motor.transform.position + Vector3.up * playerRadius, playerRadius, Vector3.down, out _, groundedDistanceCheck, ground);
    }
}
