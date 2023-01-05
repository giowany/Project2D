using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics.SymbolStore;

public class EnemyBase : MonoBehaviour
{
    public HealthBase health;
    public int damage = 10;
    public Animator animator;
    public string atack = "Atack";
    public string run = "Run";
    public string playerTag = "Player";
    public float speed = 20f;
    public Rigidbody2D rigidEnemy;
    public Transform enemySprit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthPlayer = collision.gameObject.GetComponent<HealthBase>();
        var playerController = collision.gameObject.GetComponent<PlayerController>();

        if (healthPlayer != null && playerController != null)
        {
            if(health.isDead) return;

            healthPlayer.Damage(damage);
            Attack();
        }
    }

    public void FollowPlayer(float x, bool a)
    {
        if (health.isDead) return;
        rigidEnemy.velocity = new Vector2(x * Time.deltaTime * speed * 100, 0);
        animator.SetBool(run, a);
    }

    public void LookSwap(float x)
    {
        enemySprit.DOScaleX(x, .3f);
    }

    private void Attack()
    {
        animator.SetTrigger(atack);
    }

    public void Damage(int amout = 1)
    {
        health.Damage(amout);
    }
}
