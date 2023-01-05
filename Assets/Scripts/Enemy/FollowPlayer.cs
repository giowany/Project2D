using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public EnemyBase enemy;
    public string tagPlayer = "Player";
    public float side = 1f;
    public bool anim = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagPlayer))
        {
            anim = true;
            enemy.FollowPlayer(side, anim);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagPlayer))
        {
            anim = !anim;
            enemy.FollowPlayer(0f, anim);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagPlayer))
            enemy.LookSwap(side);
    }
}
