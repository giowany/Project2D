using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;

    public float speed;
    public float jumpForce;
    public Vector2 friction = new Vector2(.1f, 0);

    private bool _jumping = false;

    private void HandleMoviment()
    {
        playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime * 100, playerRigidBody.velocity.y);

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
            _jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _jumping = false;
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
        _jumping = false;
    }
}
