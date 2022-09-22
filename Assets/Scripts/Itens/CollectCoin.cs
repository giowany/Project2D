using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : CollectBase
{
    private ParticleSystem _coin;

    private void Awake()
    {
        _coin = GetComponentInChildren<ParticleSystem>();
    }
    private void DisableItem()
    {
        gameObject.SetActive(false);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.instance.AddCoins();
        _coin.Play();
        Invoke("DisableItem", _coin.main.duration);
    }

}
