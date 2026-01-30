using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private LookSettings lookSettings;

    private PlayerInput input;
    private PlayerMotor motor;

    private float pitch;
    private float yaw;

    private float mouseSensitivity;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        motor = GetComponent<PlayerMotor>();
    }

    private void Start()
    {
        mouseSensitivity = lookSettings.Sensitivity;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        motor.SetInput(input.FrameInput);
        HandleLook(input.FrameInput.Look);
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
