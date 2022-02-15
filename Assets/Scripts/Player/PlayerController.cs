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
    public string landingBool = "Landing";


    [Header("Animation Jump Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float durationAnimation = 1f;

    [Header("Animation fall Setup")]
    public float fallScaleX = 1.2f;
    public float fallScaleY = .7f;
    public float durationAnimFall = 1f;
    public Ease ease = Ease.OutBack;

    private bool _jumping = false;
    private bool _Anim = false;

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

        if (Input.GetButtonDown("Jump"))
        {
            playerRigidBody.velocity = Vector2.up * jumpForce;
            if (playerRigidBody.transform.localScale.x > 0) playerRigidBody.transform.localScale = Vector2.one;
            else if (playerRigidBody.transform.localScale.x < 0) playerRigidBody.transform.localScale = new Vector2(-1, 1);
            DOTween.Kill(playerRigidBody.transform);
            HandleAnimationJump();
            animatorPlayer.SetBool(jumpBool, true);
            _jumping = true;
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
        _jumping = false;

        if (!_Anim)
        {
            animatorPlayer.SetBool(landingBool, true);
            animatorPlayer.SetBool(jumpBool, false);
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
            _Anim = true;
        }
    }

    private void Init()
    {
        _Anim = false;
        _jumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _jumping = true;
        animatorPlayer.SetBool(landingBool, false);
    }

    void Update()
    {
        HandleJump();
        HandleMoviment();
    }
    private void OnValidate()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        
    }
}
