//using UnityEngine;

//public class PlayerAirMovement : IMovement
//{
//    private float playerRadius = 0.5f;
//    private float groundedDistanceCheck = 0.1f;

//    private LayerMask ground;
//    private float acceleration;

//    public void Enter(PlayerMotor motor)
//    {
//        ground = motor.GroundLayer;
//        acceleration = motor.AirAcceleration;
//    }

//    public void Tick(PlayerMotor motor, FrameInput input, float dt)
//    {
//        Vector3 moveDir = new Vector3(input.Move.x, 0f, input.Move.y);
//        Vector3 horizontal = Vector3.ProjectOnPlane(motor.CurrentVelocity, Vector3.up);

//        if (horizontal.sqrMagnitude > 0.001f && moveDir.sqrMagnitude > 0.001f)
//        {
//            Vector3 desiredDir = (motor.transform.rotation * moveDir.normalized).normalized;

//            horizontal = Vector3.RotateTowards(horizontal, desiredDir * horizontal.magnitude, acceleration * dt, 0f);
//        }

//        motor.CurrentVelocity = horizontal + Vector3.up * motor.CurrentVelocity.y;
//        motor.CurrentVelocity += motor.Gravity * dt;

//        motor.transform.position += motor.CurrentVelocity * dt;

//        if (CheckGrounded(motor) && motor.CurrentVelocity.y <= 0f)
//            motor.SetMoveState(new PlayerGroundMovement());
//    }

//    public void Exit(PlayerMotor motor)
//    {
        
//    }

//    private bool CheckGrounded(PlayerMotor motor)
//    {
//        return Physics.SphereCast(motor.transform.position + Vector3.up * playerRadius, playerRadius, Vector3.down, out _, groundedDistanceCheck, ground);
//    }
//}
