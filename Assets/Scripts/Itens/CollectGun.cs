using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGun : CollectBase
{
    public SpriteRenderer sprite;
    public GunBase gun;


    protected override void Collect()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            sprite.gameObject.SetActive(false);
            gun.playerRef = player.transform;
            gun._isUsing = true;
            gun.GetComponent<Collider2D>().enabled = false;
            transform.parent = player.gun;
            transform.position = player.gun.position;
        }
    }

    private void Awake()
    {
        gun._isUsing = false;
    }
}