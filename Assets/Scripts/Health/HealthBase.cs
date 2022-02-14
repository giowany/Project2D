using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife = 10;
    public float timeToDie = 1f;

    private int _currenteLife;
    private bool _isDead = false;

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currenteLife -= damage;

        if (_currenteLife <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        _isDead = true;
        Destroy(gameObject, timeToDie);
    }

    private void Awake()
    {
        _currenteLife = startLife;
    }
}
