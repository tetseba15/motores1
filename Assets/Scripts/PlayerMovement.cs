using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerInputHandler _inputHandler;

    [Header("Movement Config")]
    [field: SerializeField, Tooltip("Base Speed")]
    public float WalkSpeed { get; private set; } = 3f;

    [field: SerializeField, Tooltip("Sprint speed")]
    public float SprintSpeed { get; private set; } = 6f;



    [Header("Physics Config")]
    [SerializeField] private float gravity = -9.81f;
    private Vector3 _velocity; 
    private bool _isGrounded;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();

    }


    void Update()
    {
        HandleMovement();
        HandleGravity();
    }


    private void HandleMovement()
    {
        Vector2 input = _inputHandler.MoveInput;
        
        Vector3 moveDirection = transform.right * input.x + transform.up * input.y;

        float currentSpeed = _inputHandler.IsSprinting ? SprintSpeed : WalkSpeed;

        _controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
    private void HandleGravity()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }

        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

    }
}
