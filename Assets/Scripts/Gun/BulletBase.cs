using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector3 foward;
    public float sideRef;

    public float timeToLive = 2f;
    private int side;

    void Update()
    {
        Foward();
    }

    private void Start()
    {
        var i = (int)Math.Round(sideRef);
        if (i == 0)
            Destroy(gameObject);
        else
            Destroy(gameObject, timeToLive);
    }

    void Foward()
    {
        side = (int) Math.Round(sideRef);
        transform.Translate(foward * Time.deltaTime * side);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyBase>();
        if(enemy != null)
        {
            enemy.Damage();
            Destroy(gameObject);
        }

        var b = collision.GetComponent<BarrierProtection>();
        if(b != null)
        {
            Destroy(gameObject);
        }
    }
}
