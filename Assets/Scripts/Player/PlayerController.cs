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

    [SerializeField]private bool _jumping = false;
    private bool _Anim = false;
    private bool _grounded;

    private void HandleMoviment()
    {
        if (!Input.GetButton("Run") && Input.GetButton("Horizontal"))
        {
            playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime * 100, playerRigidBody.velocity.y);
            if (Input.GetAxis("Horizontal") > 0) playerRigidBody.transform.DOScaleX(1, swapDuration);
            else if (Input.GetAxis("Horizontal") < 0) playerRigidBody.transform.DOScaleX(-1, swapDuration);
            animatorPlayer.SetBool(runBool, true);
            animatorPlayer.speed = speedWalkAnim;
        }
        else if (Input.GetButton("Run") && Input.GetButton("Horizontal"))
        {
            playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedRun * Time.deltaTime * 100, playerRigidBody.velocity.y);
            if (Input.GetAxis("Horizontal") > 0) playerRigidBody.transform.DOScaleX(1, swapDuration);
            else if (Input.GetAxis("Horizontal") < 0) playerRigidBody.transform.DOScaleX(-1, swapDuration);
            animatorPlayer.SetBool(runBool, true);
            animatorPlayer.speed = speedRunAnim;
        }

        else
        {
            animatorPlayer.SetBool(runBool, false);
            animatorPlayer.speed = speedWalkAnim;
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
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

    private void HandleJump()
    {
        if (_jumping) return;

        if (Input.GetButtonDown("Jump") && _grounded)
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;
            if (playerRigidBody.transform.localScale.x > 0) playerRigidBody.transform.localScale = Vector2.one;
            else if (playerRigidBody.transform.localScale.x < 0) playerRigidBody.transform.localScale = new Vector2(-1, 1);
            DOTween.Kill(playerRigidBody.transform);
            HandleAnimationJump();
            animatorPlayer.SetBool(jumpBool, true);
            _jumping = true;
            _grounded = false;
        }
    }

    private void HandleAnimationJump()
    {
        if (playerRigidBody.transform.localScale.x > 0)
        {
            playerRigidBody.transform.DOScaleY(jumpScaleY, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            playerRigidBody.transform.DOScaleX(jumpScaleX, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);

        }
        else if (playerRigidBody.transform.localScale.x < 0)
        {
            playerRigidBody.transform.DOScaleY(jumpScaleY, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            playerRigidBody.transform.DOScaleX(-jumpScaleX, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);

        }
        _Anim = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var c = collision.GetContact(0).normal;
        if (c == new Vector2(0, 1))
        {
            _grounded = true;
            if (!_Anim)
            {
                _Anim = true;
                _jumping = false;
                animatorPlayer.SetBool(fallBool, false);
                animatorPlayer.SetTrigger(landingTrigger);
                animatorPlayer.SetBool(jumpBool, false);
                // condicional para animaçao DOTween
                if (playerRigidBody.transform.localScale.x > 0) playerRigidBody.transform.localScale = Vector2.one;
                else if (playerRigidBody.transform.localScale.x < 0) playerRigidBody.transform.localScale = new Vector2(-1, 1);
                DOTween.Kill(playerRigidBody.transform);
                if (playerRigidBody.transform.localScale.x > 0)
                {
                    playerRigidBody.transform.DOScaleX(fallScaleX, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                    playerRigidBody.transform.DOScaleY(fallScaleY, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);

                }
                else if (playerRigidBody.transform.localScale.x < 0)
                {
                    playerRigidBody.transform.DOScaleX(-fallScaleX, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                    playerRigidBody.transform.DOScaleY(fallScaleY, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);
                }
            }

        }
        else
        {
            _jumping = false;
        }
    }

    private void Fall()
    {
        if (animatorPlayer.GetBool(fallBool))
        {
            _jumping = true;
            _Anim = false;
        }
    }

    private void Init()
    {
        _Anim = false;
        _jumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (playerRigidBody.velocity.y < 0)
        {
            animatorPlayer.SetBool(fallBool, true);
            _grounded = false;
        }

        if (_grounded) return;

        _jumping = true;
        
    }

    void Update()
    {
        HandleJump();
        HandleMoviment();
        Fall();
    }
    private void OnValidate()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        Init();
    }
}
