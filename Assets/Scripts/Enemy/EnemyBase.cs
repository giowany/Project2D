using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var healthPlayer = collision.gameObject.GetComponent<HealthBase>();

        if (healthPlayer != null)
        {
            healthPlayer.Damage(damage);
        }
    }
}
