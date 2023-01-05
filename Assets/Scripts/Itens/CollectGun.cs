using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGun : CollectBase
{
    public SpriteRenderer sprite;
    public GunBase gun;
    public GunAudioBase gunAudio;


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
            gun.isUsing = true;
            gun.GetComponent<Collider2D>().enabled = false;
            transform.parent = player.gun;
            transform.position = player.gun.position;
            gunAudio.PlayAudioReload();
        }
    }

    private void Awake()
    {
        gun.isUsing = false;
    }
}
