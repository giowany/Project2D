using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("References")]
    public Animator playeranimator;
    public FlashDamage flashDamage;

    public int startLife = 10;
    public float timeToDie = 1f;
    public string healthFloat = "Health";
    public string deadBool = "Dead";
    public float invensibleTime = 1f;

    private int _currenteLife;
    private bool _isDead = false;
    private bool _isInvencible = false;

    public void Damage(int damage)
    {
        if (_isDead) return;
        if (_isInvencible) return;

        _currenteLife -= damage;

        if (_currenteLife <= 0)
        {
            var a = GetComponent<PlayerController>();
            if (a != null) a.GetComponent<PlayerController>().Dead(true);
            Kill();
            Invoke(nameof(SetDead), .1f);
        }

        else if (_currenteLife >= 5)
        {
            flashDamage.Flash();
            _isInvencible = true;
            Invoke(nameof(InvensibleOff), invensibleTime);
        }
    }

    private void InvensibleOff()
    {
        _isInvencible = false;
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
