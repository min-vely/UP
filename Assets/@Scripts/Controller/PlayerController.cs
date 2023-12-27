using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 curMovementInput;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask groundLayerMask;

    [Header("Look")]
    [SerializeField]
    private Transform cameraContainer;
    [SerializeField]
    private float minXLook;
    [SerializeField]
    private float maxXLook;
    private float camCurXRot;
    [SerializeField]
    private float lookSensitivity;

    private Vector2 mouseDelta;
    private Vector3 checkPoint = new(0f, 2f, 0f);

    [HideInInspector]
    public bool canLook = true;



    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        }
    }

    private bool IsGrounded()
    {
        var transform1 = transform;
        var forward = transform1.forward;
        var position = transform1.position;
        var right = transform1.right;

        Ray[] rays =
        {
            new(position + forward * 0.2f + (Vector3.up * 0.1f) , Vector3.down),
            new(position + -forward * 0.2f+ (Vector3.up * 0.1f), Vector3.down),
            new(position + right * 0.2f + (Vector3.up * 0.1f), Vector3.down),
            new(position + -right * 0.2f + (Vector3.up * 0.1f), Vector3.down),
        };

        foreach (var ray in rays)
        {
            if (!Physics.Raycast(ray, out var hit, 0.2f, groundLayerMask)) continue;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
    }

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }
}