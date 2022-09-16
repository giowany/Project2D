using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public HealthBase health;
    public int damage = 10;
    public Animator animator;
    public string atack = "Atack";
    public string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthPlayer = collision.gameObject.GetComponent<HealthBase>();
        var playerController = collision.gameObject.GetComponent<PlayerController>();

        if (healthPlayer != null && playerController != null)
        {
            healthPlayer.Damage(damage);
            Attack();
        }
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
