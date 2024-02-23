using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using System;

public class Player : MonoBehaviour
{
    private Player _player;
    public PlayerControls controls { get; private set; }
    private CharacterController _characterController;
    public Animator animator { get; private set; }
    private Transform cameraTransform;

    [Header("Movment")]
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] public Vector2 moveInput { get; private set; }
    [SerializeField] private bool isGrounded;
    [SerializeField] private float walkSpeed = 1.5f;
    // [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float jumpHeight = 1.04f;
    private float gravity = -9.81f;
    private float verticalVelocity;


    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkingState walkingState { get; private set; }
    private void Awake()
    {
        controls = new PlayerControls();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkingState = new PlayerWalkingState(this, stateMachine, "Walking");
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
        isGrounded = _characterController.isGrounded;
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        
        // off set mouse input by the cameras world position
        movementDirection = movementDirection.x * cameraTransform.right.normalized + movementDirection.z * cameraTransform.forward.normalized;
        movementDirection.y = 0f;
        

        if (moveInput.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }

        ApplyGravity();

        // Rotate toward the camera
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Animation();
    }

    private void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 appliedVelocity = new Vector3(0, verticalVelocity, 0);

        _characterController.Move(appliedVelocity * Time.deltaTime);
    }
    private void Jump()
    {
        if (isGrounded)
            verticalVelocity += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    public void Animation()
    {
        animator.SetFloat("xInput", moveInput.x, .1f, Time.deltaTime);
        animator.SetFloat("yInput", moveInput.y, .1f, Time.deltaTime);
    }

    #region Configuration
    private void Configure()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraTransform = Camera.main.transform;

        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        AssignInputEvents();
    }

    private void AssignInputEvents()
    {
        controls = _player.controls;

        controls.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Player.Move.canceled += context => moveInput = Vector2.zero;

        controls.Player.Jump.performed += _ => Jump();
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
