using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    public float invensibleTime = 1f;
    private bool _isInvencible = false;

    public override void Damage(int damage)
    {
        if (_isInvencible) return;

        base.Damage(damage);

        if (_currenteLife < startLife)
        {
            _isInvencible = true;
            Invoke(nameof(InvensibleOff), invensibleTime);
        }
    }

    private void InvensibleOff()
    {
        _isInvencible = false;
    }
}
