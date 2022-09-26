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

    protected int _currenteLife;
    public bool isDead = false;

    public virtual void Damage(int damage)
    {
        if (isDead) return;

        _currenteLife -= damage;

        if (_currenteLife <= 0)
        {
            var a = GetComponent<PlayerController>();
            if (a != null) a.GetComponent<PlayerController>().Dead(true);
            Kill();
            Invoke(nameof(SetDead), .1f);
        }

        else if (_currenteLife < startLife)
        {
            flashDamage.Flash();
        }
    }


    private void SetDead()
    {
        playeranimator.SetBool(deadBool, true);
    }

    private void Kill()
    {
        isDead = true;
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
