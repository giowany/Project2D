using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;

    public float speed;

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            playerRigidBody.velocity = new Vector2(speed * Time.deltaTime * 100, playerRigidBody.velocity.y);
        else if (Input.GetKey(KeyCode.LeftArrow))
            playerRigidBody.velocity = new Vector2(-speed * Time.deltaTime * 100,playerRigidBody.velocity.y);
    }

    void Update()
    {
        HandleMoviment();
    }
    private void OnValidate()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
}
