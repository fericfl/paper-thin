using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;

    private string currentState;
    private Vector2 lastDirection = Vector2.down;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        UpdateAnimation();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            lastDirection = moveInput;
        }
    }

    public void UpdateAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            if (moveInput.x > 0 && moveInput.y > 0) ChangeAnimationState("Run_Right_Up");
            else if (moveInput.x > 0 && moveInput.y < 0) ChangeAnimationState("Run_Right_Down");
            else if (moveInput.x < 0 && moveInput.y > 0) ChangeAnimationState("Run_Left_Up");
            else if (moveInput.x < 0 && moveInput.y < 0) ChangeAnimationState("Run_Left_Down");
            
            else if (moveInput.x > 0) ChangeAnimationState("Run_Right");
            else if (moveInput.x < 0) ChangeAnimationState("Run_Left");
            else if (moveInput.y > 0) ChangeAnimationState("Run_Up");
            else if (moveInput.y < 0) ChangeAnimationState("Run_Down");
        }
        else
        {
            if (lastDirection.y > 0)
            {
                ChangeAnimationState("Idle_Up");
            }
            else
            {
                ChangeAnimationState("Idle_Down");
            }
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
