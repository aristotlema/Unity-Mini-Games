using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    public PlayerControls controls;

    public Animator animator;


    private Player _player;
    private PlayerControls _controls;
    private CharacterController _characterController;

    private Vector2 moveInput;
    private Vector3 movementDirection;
    [SerializeField] private float walkSpeed = 1.5f;

    public PlayerStateMachine stateMachine {  get; private set; }
    public LocomotionState locomotionState { get; private set; }
    private void Awake()
    {
        controls = new PlayerControls();
        stateMachine = new PlayerStateMachine();

        locomotionState = new LocomotionState(this, stateMachine, "Locomotion");
    }

    private void Start()
    {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();

        AssignInputEvents();

        stateMachine.Initialize(locomotionState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void HandleMovement()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (movementDirection.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * walkSpeed);
        }
    }

    private void AssignInputEvents()
    {
        _controls = _player.controls;

        _controls.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        _controls.Player.Move.canceled += context => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDestroy()
    {
        controls.Disable();
    }
}
