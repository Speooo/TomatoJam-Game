using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private LookSettings lookSettings;

    private PlayerInput input;
    private PlayerMotor motor;
    private PlayerCamera playerCam;
    private PlayerInteract interact;

    private float pitch;
    private float yaw;

    private float mouseSensitivity;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        motor = GetComponent<PlayerMotor>();
        playerCam = GetComponent<PlayerCamera>();
        interact = GetComponent<PlayerInteract>();

        playerCam.enabled = false;
    }

    private void Start()
    {
        mouseSensitivity = lookSettings.Sensitivity;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // send inputs over to motor
        motor.SetInput(input.FrameInput);

        // check left click for camera flash
        playerCam.HandleCamera(input.FrameInput.Attack);

        // move camera / player transform from mouse delta
        HandleLook(input.FrameInput.Look);

        // check for interact key
        interact.HandleInteract(input.FrameInput.Interact);
    }

    private void HandleLook(Vector2 mouseDelta)
    {
        // raw pitch delta
        pitch -= mouseDelta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, lookSettings.MinAngle, lookSettings.MaxAngle);

        // raw yaw delta
        yaw += mouseDelta.x * mouseSensitivity;

        cameraPivot.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);
    }
    [ContextMenu("pick up camera")]
    public void PickUpCamera()
    {
        playerCam.enabled = true;
    }
}

[System.Serializable]
public struct LookSettings
{
    [Header("Mouse Settings")]
    [Range(0.01f, 0.5f)]
    [SerializeField] private float sensitivity;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    public float Sensitivity => sensitivity;
    public float MinAngle => minAngle;
    public float MaxAngle => maxAngle;
}
