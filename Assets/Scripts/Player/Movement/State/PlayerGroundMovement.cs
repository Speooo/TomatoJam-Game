//using UnityEngine;

//public class PlayerGroundMovement : IMovement
//{
//    private float playerRadius = 0.5f;
//    private float groundedDistanceCheck = 0.1f;
//    private float groundSnapDistance = 5f;
//    private float skin = 0.01f;
//    private float maxSlopeAngle = 55f;

//    private LayerMask ground;
//    private float moveSpeed;
//    private float jumpForce;

//    private RaycastHit groundHit;

//    public void Enter(PlayerMotor motor)
//    {
//        ground = motor.GroundLayer;
//        moveSpeed = motor.MovementSpeed;
//        jumpForce = motor.JumpForce;

//        SnapToGround(motor);
//        motor.CurrentVelocity.y = 0f;
//    }

//    public void Tick(PlayerMotor motor, FrameInput input, float dt)
//    {
//        bool grounded = CheckGrounded(motor);

//        if (grounded)
//        {
//            float slopeAngle = Vector3.Angle(groundHit.normal, Vector3.up);
//            if (slopeAngle > maxSlopeAngle)
//                grounded = false;
//        }

//        Vector3 up = grounded ? groundHit.normal : Vector3.up;
//        Vector3 camForward = motor.transform.forward;
//        Vector3 camRight = motor.transform.right;

//        Vector3 slopeForward = Vector3.ProjectOnPlane(camForward, up).normalized;
//        Vector3 slopeRight = Vector3.ProjectOnPlane(camRight, up).normalized;

//        Vector3 desired = (slopeForward * input.Move.y + slopeRight * input.Move.x) * moveSpeed;
//        Debug.Log(grounded);
//        if (grounded)
//        {
//            float nVel = Vector3.Dot(motor.CurrentVelocity, groundHit.normal);
//            if (nVel < 0f)
//                motor.CurrentVelocity -= groundHit.normal * nVel;

//            float nVelAfter = Vector3.Dot(motor.CurrentVelocity, groundHit.normal);

//            if (nVelAfter > 0f)
//            {
//                motor.CurrentVelocity -= groundHit.normal * nVelAfter;
//                motor.CurrentVelocity += -groundHit.normal * 0.1f;
//            }

//            Vector3 planarMove = Vector3.ProjectOnPlane(desired, groundHit.normal);
//            motor.CurrentVelocity.x = planarMove.x;
//            motor.CurrentVelocity.z = planarMove.z;
//        }
//        else
//        {
//            motor.CurrentVelocity.x = desired.x;
//            motor.CurrentVelocity.z = desired.z;
//        }

//        //motor.CurrentVelocity.x = move.x;
//        //motor.CurrentVelocity.z = move.z;

//        if (grounded )
//        {
//            if (input.Jump)
//            {
//                motor.CurrentVelocity.y = jumpForce;
//                motor.SetMoveState(new PlayerAirMovement());
//            }
//        }
//        else
//        {
//            motor.SetMoveState(new PlayerAirMovement());
//        }

//        motor.transform.position += motor.CurrentVelocity * dt;

//        if (grounded && !input.Jump)
//        {
//            SnapToGround(motor);
//        }
//    }

//    public void Exit(PlayerMotor motor)
//    {
        
//    }

//    private void SnapToGround(PlayerMotor motor)
//    {
//        Vector3 origin = motor.transform.position + Vector3.up * playerRadius;

//        if (Physics.SphereCast(
//            origin,
//            playerRadius,
//            Vector3.down,
//            out RaycastHit hit,
//            groundSnapDistance,
//            ground))
//        {
//            motor.transform.position = hit.point + hit.normal * (playerRadius + skin);
//        }
//    }

//    private bool CheckGrounded(PlayerMotor motor)
//    {
//        return Physics.SphereCast(motor.transform.position + Vector3.up * playerRadius, playerRadius, Vector3.down, out groundHit, groundedDistanceCheck, ground);
//    }
//}
