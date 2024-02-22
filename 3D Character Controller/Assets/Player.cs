using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;

public class Player : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public PlayerControls controls { get; private set; }

    public Animator animator;

    private Player _player;
    private PlayerControls _controls;
    private CharacterController _characterController;
    public PlayerAim aim { get; private set; }

    public Vector2 moveInput { get; private set; }
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed = 1.5f;

    private Transform cameraTransform;

    private Vector2 aimInput;
    [SerializeField] private float rotationSpeed = 20f;

    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkingState walkingState { get; private set; }
    public LocomotionState locomotionState { get; private set; }
    private void Awake()
    {
        controls = new PlayerControls();
        aim = new PlayerAim();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkingState = new PlayerWalkingState(this, stateMachine, "Walking");
        locomotionState = new LocomotionState(this, stateMachine, "Locomotion");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Configure();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void HandleMovement()
    {
        Vector3 movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // off set mouse input by the cameras world position
        movementDirection = movementDirection.x * cameraTransform.right.normalized + movementDirection.z * cameraTransform.forward.normalized;
        movementDirection.y = 0f;

        if (moveInput.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }

        // Rotate toward the camera
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Animation();
    }

    public void Animation()
    {
        animator.SetFloat("xInput", moveInput.x, .1f, Time.deltaTime);
        animator.SetFloat("yInput", moveInput.y, .1f, Time.deltaTime);
    }

    #region Configuration
    private void Configure()
    {
        cameraTransform = Camera.main.transform;

        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        AssignInputEvents();
    }

    private void AssignInputEvents()
    {
        _controls = _player.controls;

        _controls.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => moveInput = Vector2.zero;

        _controls.Player.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        _controls.Player.Aim.canceled += context => aimInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDestroy()
    {
        controls.Disable();
    }
    #endregion
}
