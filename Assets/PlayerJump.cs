using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] InputAction JumbButtonSpace;
    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    void Start()
    {
        JumbButtonSpace.Enable();
    }
    private void Update()
    {
        bool _isGrounded = IsGrounded();

        if ((JumbButtonSpace.IsPressed()||jumpButton.action.WasPressedThisFrame()) && _isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }
}
