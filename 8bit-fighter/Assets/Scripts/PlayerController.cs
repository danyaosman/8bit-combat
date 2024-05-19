using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f,
        runSpeed = 7f;
    Vector2 moveInput;

    public float CurrentMoveSpeed
    {
        get
        {
            if(IsMoving)
            {
                if(IsRunning)
                {
                    return runSpeed;
                }
                return walkSpeed;
            } return 0;
        }
    }


    [SerializeField]
    private bool _isMoving = false;

    //this property will be used to apply the animation
    //based on the action of the player
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set { 
            _isMoving = value;
            animator.SetBool("isMoving", value);
        } 
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set 
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight {get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //flipping local scale
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    //awake happens before start
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        } else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        //facing right
        if(moveInput.x>0 && !IsFacingRight)
        {

            IsFacingRight = true;

        } else if (moveInput.x<0 && IsFacingRight) //facing left
        {

            IsFacingRight=false;
        }
    }
}
