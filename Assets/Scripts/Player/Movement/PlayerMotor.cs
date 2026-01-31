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
    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;

    private CharacterController controller;
    private FrameInput input;

    private Vector3 velocity;
    private float groundedDistanceCheck = 0.2f;

    private float footstepTriggerTimer;
    private float footstepInterval = 0.5f;

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

        if (input.Move.sqrMagnitude > 0.001f)
        {
            PlayFoostepAudio();
        }

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

    private void PlayFoostepAudio()
    {
        footstepTriggerTimer -= Time.deltaTime;
        if (footstepTriggerTimer < 0f)
        {
            int index = Random.Range(0, 2);

            if (index == 0)
                AudioManager.Instance.PlaySfx2D(footstep1, 0.25f);
            else
                AudioManager.Instance.PlaySfx2D(footstep2, 0.25f);

            footstepTriggerTimer = footstepInterval;
        }
    }

    private bool CheckGrounded() 
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundedDistanceCheck, ground, QueryTriggerInteraction.Ignore); 
    }
}
