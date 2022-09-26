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
    public SOPlayerConfig playerConfig;
    public Transform gun;
    public ParticleSystem dustVFX;

    [Header("Jump Setings")]
    public Collider2D pCollider2D;
    public float bound;
    public float distanceToGround = .1f;
    public ParticleSystem jumpVFX;

    private bool _Anim = false;
    private bool _grounded;
    private float _currentSpeed;
    private bool _facingRight = true;
    private bool _dead = false;

    private void HandleMoviment()
    {
        if (_dead) return;
        if (!Input.GetButton("Run"))
        {
            _currentSpeed = playerConfig.speed;
            animatorPlayer.speed = playerConfig.speedWalkAnim;
        }

        else
        {
            _currentSpeed = playerConfig.speedRun;
            animatorPlayer.speed = playerConfig.speedRunAnim;
        }

        float axis = Input.GetAxis("Horizontal");
        bool walking = Input.GetButton("Horizontal");

        playerRigidBody.velocity = new Vector2(axis * _currentSpeed * Time.deltaTime * 100, playerRigidBody.velocity.y);
        animatorPlayer.SetBool(playerConfig.runBool, walking);

        if (walking)
        {
            if(axis > 0 && !_facingRight)
            {
                _facingRight = true;
                playerRigidBody.transform.DOScaleX(1, playerConfig.swapDuration);
            }

            else if (axis < 0 && _facingRight)
            {
                _facingRight = false;
                playerRigidBody.transform.DOScaleX(-1, playerConfig.swapDuration);
            }

        }

        if(playerRigidBody.velocity.x < 0)
        {
            playerRigidBody.velocity += playerConfig.friction;
        }
        else if (playerRigidBody.velocity.x > 0)
        {
            playerRigidBody.velocity -= playerConfig.friction;
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
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRigidBody.velocity = Vector2.up * playerConfig.jumpForce;

            HandleAnimationJump(playerConfig.jumpScaleX, playerConfig.jumpScaleY, playerConfig.durationAnimation);

            animatorPlayer.SetBool(playerConfig.jumpBool, true);
            dustVFX.Stop();
            JumpVFX();
            _grounded = false;

        }
    }

    private void JumpVFX()
    {
        jumpVFX.Play();
    }

    private void HandleAnimationJump(float xScale, float yScale, float duration)
    {
        ResetScale();
        playerRigidBody.transform.DOScaleY(yScale, duration).SetLoops(2, LoopType.Yoyo).SetEase(playerConfig.ease);
        playerRigidBody.transform.DOScaleX(xScale * XSign(), duration).SetLoops(2, LoopType.Yoyo).SetEase(playerConfig.ease);

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
                animatorPlayer.SetTrigger(playerConfig.landingTrigger);
                animatorPlayer.SetBool(playerConfig.jumpBool, false);
                
                HandleAnimationJump(playerConfig.fallScaleX, playerConfig.fallScaleY, playerConfig.durationAnimFall);
            }

            dustVFX.Play();
        }
    }

    private void Fall()
    {
        animatorPlayer.SetBool(playerConfig.fallBool, playerRigidBody.velocity.y < 0);
        if (animatorPlayer.GetBool(playerConfig.fallBool) && _grounded)
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

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, bound + distanceToGround);
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
        if(pCollider2D != null)
        {
            bound = pCollider2D.bounds.extents.y;
        }
    }
}
