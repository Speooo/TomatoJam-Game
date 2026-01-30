using UnityEngine;

public interface IMovement
{
    void Enter(PlayerMotor motor);
    void Tick(PlayerMotor motor, FrameInput input, float dt);
    void Exit(PlayerMotor motor);
}
