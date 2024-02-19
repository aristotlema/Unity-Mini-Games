using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public PlayerControls controls { get; private set; }

    public Animator animator;

    private Player _player;
    private PlayerControls _controls;
    private CharacterController _characterController;
    public PlayerAim aim { get; private set; }

    private Vector2 moveInput;
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed = 1.5f;

    private Vector2 aimInput;

    public PlayerStateMachine stateMachine {  get; private set; }
    public LocomotionState locomotionState { get; private set; }
    private void Awake()
    {
        controls = new PlayerControls();
        stateMachine = new PlayerStateMachine();
        aim = new PlayerAim();

        locomotionState = new LocomotionState(this, stateMachine, "Locomotion");

    }

    private void Start()
    {
        Configure();
        stateMachine.Initialize(locomotionState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void HandleMovement()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // Vector2 lookingDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(aimInput.x, 0, aimInput.y).normalized;

        // Ray ray = Camera.main.ScreenPointToRay(aimInput);

        //Camera.main.transform.localEulerAngles = new Vector3(aimInput.x, aimInput.y, 0);

        // Camera.main.transform.position = transform.position - Camera.main.transform.forward * 5;

        if (movementDirection.magnitude > 0)
        {
            //Quaternion desiredRotation = Quaternion.LookRotation(lookingDirection, Vector3.up);

            //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime);

            _characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }
    }

    public void HandleAim()
    {
        
        //Vector2 lookingDirection = aimInput - new Vector2(transform.position.z, transform.position.y);
        //lookingDirection.Normalize();
        //Debug.Log(lookingDirection);
    }

    public void HandleRotation()
    {
        //Vector3 lookingDirection = movementDirection * Time.deltaTime * walkSpeed;
    }

    #region Configuration
    private void Configure()
    {
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