using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
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

    private bool _isStunned;
    private bool _wasStunned;
    private bool _slider = false;
    private float _pushForce;
    [SerializeField] private float gravity = 9.8f;
    private Vector3 _pushDir;

    private Vector2 mouseDelta;
    private Vector3 checkPoint = new(0f, 2f, 0f); // 플레이어 초기 시작 장소

    private Rigidbody _rigidbody;

    private ICommand moveCommand;
    private ICommand jumpCommand;
    private ICommand lookCommand;

    #endregion

    #region Properties

    public float MoveSpeed
    {
        get { return moveSpeed; }
        private set { moveSpeed = value; }
    }
    public float JumpForce
    {
        get { return jumpForce; }
        private set { jumpForce = value; }
    }
    public LayerMask GroundLayerMask
    {
        get { return groundLayerMask; }
        private set { groundLayerMask = value; }
    }
    public Transform CameraContainer
    {
        get { return cameraContainer; }
        private set { cameraContainer = value; }
    }
    public float MinXLook
    {
        get { return minXLook; }
        private set { minXLook = value; }
    }
    public float MaxXLook
    {
        get { return maxXLook; }
        private set { maxXLook = value; }
    }
    public float CamCurXRot
    {
        get { return camCurXRot; }
        private set { camCurXRot = value; }
    }
    public float LookSensitivity
    {
        get { return lookSensitivity; }
        private set { lookSensitivity = value; }
    }
    public Vector2 MouseDelta
    {
        get { return mouseDelta; }
        private set { mouseDelta = value; }
    }

    #endregion

    #region Init

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        moveCommand = new MoveCommand();
        jumpCommand = new JumpCommand();
        lookCommand = new LookCommand();
    }

    #endregion

    private void FixedUpdate()
    {
        moveCommand.Execute(this);
    }

    private void LateUpdate()
    {
        lookCommand.Execute(this);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        lookCommand.Execute(this);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // moveCommand가 MoveCommand일 경우에만 SetInput 호출
            if (moveCommand is MoveCommand moveCmd)
            {
                moveCmd.SetInput(context.ReadValue<Vector2>());
            }
            moveCommand.Execute(this);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (moveCommand is MoveCommand moveCmd)
            {
                moveCmd.SetInput(Vector2.zero);
            }
            moveCommand.Execute(this);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            jumpCommand.Execute(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void HitPlayer(Vector3 velocityF, float time)
    {
        _rigidbody.velocity = velocityF;
        _pushForce = velocityF.magnitude;
        _pushDir = Vector3.Normalize(velocityF);
        StartCoroutine(Decrease(velocityF.magnitude, time));
    }

    private IEnumerator Decrease(float value, float duration)
    {
        if (_isStunned)
        {
            _wasStunned = true;
            _isStunned = true;
        }
        var delta = value / duration;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return null;
            if (!_slider) // 땅이 slide가 아니면 감소된 pushForce 적용
            {
                _pushForce -= Time.deltaTime * delta;
                _pushForce = _pushForce < 0 ? 0 : _pushForce;
            }
            _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));
        }

        if (_wasStunned) _wasStunned = false;
        else
        {
            _isStunned = false;
        }
    }


    private void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
    }

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        checkPoint = newCheckPoint;
    }

    public void UpdateCamCurXRot(float delta)
    {
        CamCurXRot += delta;
        CamCurXRot = Mathf.Clamp(CamCurXRot, MinXLook, MaxXLook);
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }
}
