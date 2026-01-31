using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityStrength;

    private CharacterController controller;
    private FrameInput input;

    private Vector3 velocity;
    private float groundedDistanceCheck = 0.2f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetInput(FrameInput frameInput)
    {
        input = frameInput;
    }

    private void Update()
    {
        HandleMovement();
        HandleGravityAndJump();
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * input.Move.x + transform.forward * input.Move.y;

        controller.Move(movementSpeed * Time.deltaTime * move);
    }

    private void HandleGravityAndJump()
    {
        if (input.Jump && CheckGrounded())
            velocity.y = jumpForce;

        velocity.y += gravityStrength * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;
    }

    private bool CheckGrounded() 
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundedDistanceCheck, ground, QueryTriggerInteraction.Ignore); 
    }
}
