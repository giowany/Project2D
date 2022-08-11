using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D playerRigidBody;
    public Animator animatorPlayer;

    [Header("Moviment Setup")]
    public float speed;
    public float speedRun;
    public float jumpForce;
    public Vector2 friction = new Vector2(.1f, 0);

    [Header("Animations Setup")]
    public string runBool = "Run";
    public float speedWalkAnim = 1f;
    public float speedRunAnim = 1.5f;
    public float swapDuration = .3f;
    public string jumpBool = "Jump";
    public string landingTrigger = "Landing";
    public string fallBool = "Fall";


    [Header("DOAnimation Jump Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float durationAnimation = 1f;

    [Header("DOAnimation fall Setup")]
    public float fallScaleX = 1.2f;
    public float fallScaleY = .7f;
    public float durationAnimFall = 1f;
    public Ease ease = Ease.OutBack;

    private bool _Anim = false;
    private bool _grounded;
    private float _currentSpeed;
    private bool _facingRight = false;
    private bool _dead = false;

    private void HandleMoviment()
    {
        if (_dead) return;
        if (!Input.GetButton("Run"))
        {
            _currentSpeed = speed;
            animatorPlayer.speed = speedWalkAnim;
        }

        else
        {
            _currentSpeed = speedRun;
            animatorPlayer.speed = speedRunAnim;
        }

        float axis = Input.GetAxis("Horizontal");
        bool walking = Input.GetButton("Horizontal");

        playerRigidBody.velocity = new Vector2(axis * _currentSpeed * Time.deltaTime * 100, playerRigidBody.velocity.y);
        animatorPlayer.SetBool(runBool, walking);

        if (walking)
        {
            if(axis > 0 && !_facingRight)
            {
                _facingRight = true;
                playerRigidBody.transform.DOScaleX(1, swapDuration);
            }

            else if (axis < 0 && _facingRight)
            {
                _facingRight = false;
                playerRigidBody.transform.DOScaleX(-1, swapDuration);
            }
        }

        if(playerRigidBody.velocity.x < 0)
        {
            playerRigidBody.velocity += friction;
        }
        else if (playerRigidBody.velocity.x > 0)
        {
            playerRigidBody.velocity -= friction;
        }
    }

    public void Dead(bool d)
    {
        if (d) _dead = true;
        if (!d) _dead = false;
    }

    public float XSign()
    {
        return Mathf.Sign(playerRigidBody.transform.localScale.x);
    }

    private void ResetScale()
    {
        playerRigidBody.transform.localScale = new Vector2(1 * XSign(), 1);

        DOTween.Kill(playerRigidBody.transform);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;

            HandleAnimationJump(jumpScaleX,jumpScaleY,durationAnimation);

            animatorPlayer.SetBool(jumpBool, true);
            _grounded = false;
        }
    }

    private void HandleAnimationJump(float xScale, float yScale, float duration)
    {
        ResetScale();
        playerRigidBody.transform.DOScaleY(yScale, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        playerRigidBody.transform.DOScaleX(xScale * XSign(), duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);

        _Anim = !_Anim;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var c = collision.GetContact(0).normal;
        if (c == new Vector2(0, 1))
        {
            _grounded = true;
            if (!_Anim)
            {
                animatorPlayer.SetTrigger(landingTrigger);
                animatorPlayer.SetBool(jumpBool, false);
                
                HandleAnimationJump(fallScaleX, fallScaleY, durationAnimFall);
            }

        }
    }

    private void Fall()
    {
        animatorPlayer.SetBool(fallBool, playerRigidBody.velocity.y < 0);
        if (animatorPlayer.GetBool(fallBool) && _grounded)
        {
            _Anim = !_Anim;
            _grounded = !_grounded;
        }
    }

    private void Init()
    {
        _Anim = true;
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJump();
        HandleMoviment();
        Fall();
    }

    private void Awake()
    {
        Init();
        Dead(false);
    }
}
