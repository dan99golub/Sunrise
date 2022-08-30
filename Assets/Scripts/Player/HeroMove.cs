using UnityEngine;
using Mirror;

[RequireComponent(typeof(AnimationService))]
[RequireComponent(typeof(AnimationService))]
public class HeroMove : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Camera _camera;
    private InputService _inputService;
    private AnimationService _animator;

    private void Awake()
    {
        _inputService = Game.InputService;
        _animator = GetComponent<AnimationService>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        _camera = Camera.main;
        _camera.GetComponent<CameraFollow>().Follow(transform);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Vector3 movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > 0.001f)
        {
            movementVector = GetDirection();
            LookAt(movementVector);
        }
        Move(movementVector * Time.deltaTime * _speed);

    }

    private Vector3 GetDirection()
    {
        Vector3 movementVector = _camera.transform.TransformDirection(_inputService.Axis);
        movementVector.z = 0;
        movementVector.Normalize();
        return movementVector;
    }

    [Server]
    private void Move(Vector3 direction)
    {
        if (!isLocalPlayer)
            return;

        if (direction == Vector3.zero)
            _animator.PlayAnimationMotion(false);

        else
            _animator.PlayAnimationMotion(true);

        _rigidbody.MovePosition(transform.position + direction);
    }

    [Server]
    private void LookAt(Vector3 direction)
    {
        if (!isLocalPlayer)
            return;

        float lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        _rigidbody.rotation = lookAngle;
    }

    [Command]
    private void CmdLookAt(Vector3 direction)
    {
        LookAt(direction);
    }

    public void RequestToTurn(Vector3 direction)
    {
        CmdLookAt(direction);
    }
}
