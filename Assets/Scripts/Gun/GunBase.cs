using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public BulletBase bullet;
    public Transform shootPoint;
    public Transform playerRef;
    public float timeToHate = .5f;
    public bool isUsing = false;
    public GunAudioBase gunAudio;
    public int bulletLimite;
    public float reloadTime = 1.5f;

    private Coroutine _currentCorotine;
    private int _currentBulletNumbers;

    void Update()
    {
        ShootGun();
        if (Input.GetKeyDown("k")) Reload();
        if (_currentBulletNumbers >= bulletLimite)
        {
            Reload();
        }
    }

    private void Awake()
    {
        _currentBulletNumbers = 0;
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

    void Reload()
    {
        if (!isUsing) return;
        _currentBulletNumbers = 0;
        UsingGun();
        gunAudio.PlayAudioReload();
        Invoke("UsingGun", reloadTime);
    }

    public void Shoot()
    {
        if(!isUsing) return;
        if(gunAudio != null) gunAudio.PlayAudio();
        var projectil = Instantiate(bullet);
        projectil.sideRef = playerRef.localScale.x;
        projectil.transform.position = shootPoint.position;
        _currentBulletNumbers++;
    }

    private void UsingGun()
    {
        isUsing = !isUsing;
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
