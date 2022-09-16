using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public BulletBase bullet;
    public Transform shootPoint;
    public Transform playerRef;
    public float timeToHate = .5f;

    private Coroutine _currentCorotine;

    void Update()
    {
        ShootGun();
    }

    public void ShootGun()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _currentCorotine = StartCoroutine(FireHate());
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            if(_currentCorotine != null) StopCoroutine(_currentCorotine);
        }
    }

    public void Shoot()
    {
        var projectil = Instantiate(bullet);
        projectil.sideRef = playerRef.localScale.x;
        projectil.transform.position = shootPoint.position;
    }

    IEnumerator FireHate()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeToHate);
        }
    }
}
