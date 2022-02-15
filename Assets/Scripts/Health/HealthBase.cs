using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("References")]
    public Animator playeranimator;

    public int startLife = 10;
    public float timeToDie = 1f;
    public string healthFloat = "Health";
    public string deadBool = "Dead";

    private int _currenteLife;
    private bool _isDead = false;

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currenteLife -= damage;

        if (_currenteLife <= 0)
        {
            Kill();
            Invoke(nameof(SetDead), .1f);
        }
    }

    private void SetDead()
    {
        playeranimator.SetBool(deadBool, true);
    }

    private void Kill()
    {
        _isDead = true;
        Destroy(gameObject, timeToDie);
        playeranimator.SetFloat(healthFloat, _currenteLife);
    }

    private void Init()
    {
        _currenteLife = startLife;
        playeranimator.SetFloat(healthFloat, _currenteLife);
        playeranimator.SetBool(deadBool, false);
    }

    private void Awake()
    {
        Init();
    }
}
