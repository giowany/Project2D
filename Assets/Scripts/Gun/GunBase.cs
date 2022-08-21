using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectile;
    public Transform gunReference;
    public Transform playerSideReference;
    public float cadenceGun = .3f;
    public KeyCode keyCode;

    private Coroutine _currentCorotine;

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(keyCode))
        {
            _currentCorotine = StartCoroutine(Cadence());
        }

        else if (Input.GetKeyUp(keyCode))
        {
            if(_currentCorotine != null)
                StopCoroutine(_currentCorotine);
        }
    }

    public void ShootingBullet()
    {
        var p = Instantiate(projectile);
        p.transform.position = gunReference.position;
        p.side = playerSideReference.transform.localScale.x;
    }

    IEnumerator Cadence()
    {
        while (true)
        {
            ShootingBullet();
            yield return new WaitForSeconds(cadenceGun);
        }
    }

}
