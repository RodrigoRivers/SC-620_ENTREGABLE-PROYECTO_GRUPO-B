using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Character2DController : MonoBehaviour
{
    readonly object _lock = new object();
    static Character2DController _instance;



    public static Character2DController Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 300.0F;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 140.0F;

    [SerializeField]
    float jumpGraceTime = 0.20F;

    [SerializeField]
    float fallMultiplier = 3.0F;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [Header("Extras")]
    [SerializeField]
    Animator animator;

    [SerializeField]
    Vector2 reboundSpeed;

    [SerializeField]
    float lostControlLifeTime = 1.0F;

    Rigidbody2D _rb;

    float _inputX;
    float _gravityY;
    float _lastTimeJumpPressed;

    bool _isMoving;
    bool _isJumpPressed;
    bool _isJumping;
    bool _canMove = true;


    void Awake()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = this;
                }
            }
        }

        _rb = GetComponent<Rigidbody2D>();
        _gravityY = -Physics2D.gravity.y;
    }

    void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleMove();
        HandleFlipX();
    }

    private void HandleJump()
    {
        if (_lastTimeJumpPressed > 0.0F && Time.time - _lastTimeJumpPressed <= jumpGraceTime)
        {
            //Coyote time: la tecla de salto se ha presionado sin embardo no se ha realizado el salto
            _isJumpPressed = true;
        }
        else 
        {
            _lastTimeJumpPressed = 0.0F;
        }


        if (_isJumpPressed)
        {
            bool isGrounded = IsGrounded();

            if (isGrounded)
            {

                AudioManager.Instance.PlaySFX("Jump");
                _rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            }
        }

        if (_rb.velocity.y < -0.01F) 
        {
            _rb.velocity -= Vector2.up * _gravityY * fallMultiplier * Time.fixedDeltaTime;
        }

        _isJumping = !IsGrounded();
    }

    private void HandleFlipX()
    {
        if (!_isMoving)        
            return;

        bool facingRight = _inputX > 0.0F;
        if (isFacingRight != facingRight) 
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }

    }

    private void HandleMove()
    {
        bool isMoving = animator.GetFloat("speed") > 0.01F;
        if (isMoving != _isMoving && !_isJumping) 
        {
            animator.SetFloat("speed",Mathf.Abs(_inputX));
        }

        bool isJumping = animator.GetBool("isJumping");
        if (isJumping != _isJumping) 
        {
            animator.SetBool("isJumping", _isJumping);
        }

        float velocityX = _inputX * moveSpeed * Time.fixedDeltaTime;
        Vector2 direction = new Vector2(velocityX, _rb.velocity.y);
        _rb.velocity = direction;

        if (_canMove)
        {
            _rb.velocity = direction;
        }

    }    

    private void HandleInputs()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _isMoving = _inputX != 0.0F;

        _isJumpPressed = Input.GetButtonDown("Jump");
        if (_isJumpPressed) 
        {
            //Comienza el calculo del Coyote Time
            _lastTimeJumpPressed = Time.time;
        }
    }

    private bool IsGrounded()
    {
        return
            Physics2D.OverlapCapsule
                (
                    groundCheck.position, new Vector2(0.63F, 0.4F),
                    CapsuleDirection2D.Horizontal, 0.0F, groundMask
                );
    }

    public void Rebound(Vector2 contactpoint)
    {
        _rb.velocity = new Vector2(-reboundSpeed.x * contactpoint.x, reboundSpeed.y);
        StartCoroutine(LoseControl());
    }

    IEnumerator LoseControl()
    {
        _canMove = false;
        yield return new WaitForSeconds(lostControlLifeTime);
        _canMove = true;
    }

}
