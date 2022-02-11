using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D playerRigidBody;
    public Image imageplayer;

    [Header("Moviment Setup")]
    public float speed;
    public float speedRun;
    public float jumpForce;
    public Vector2 friction = new Vector2(.1f, 0);

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
        if (!Input.GetButton("Run"))
            playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime * 100, playerRigidBody.velocity.y);
        else if (Input.GetButton("Run"))    
            playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedRun * Time.deltaTime * 100, playerRigidBody.velocity.y);

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
            playerRigidBody.transform.localScale = Vector2.one;
            DOTween.Kill(playerRigidBody.transform);
            HandleAnimationJump();
            _jumping = true;
        }
    }

    private void HandleAnimationJump()
    {
        playerRigidBody.transform.DOScaleY(jumpScaleY, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        playerRigidBody.transform.DOScaleX(jumpScaleX, durationAnimation).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        _Anim = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _jumping = false;

        if (!_Anim)
        {
            playerRigidBody.transform.localScale = Vector2.one;
            DOTween.Kill(playerRigidBody.transform);
            playerRigidBody.transform.DOScaleX(fallScaleX, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            playerRigidBody.transform.DOScaleY(fallScaleY, durationAnimFall).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            _Anim = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _jumping = true;
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
        _Anim = false;
        _jumping = false;
    }
}
