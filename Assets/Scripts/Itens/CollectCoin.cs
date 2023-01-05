using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : CollectBase
{
    [SerializeField]private ParticleSystem _coin;
    public AudioSource coinAudioSource;

    private void Start()
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
        coinAudioSource.Play();
        Invoke("DisableItem", _coin.main.duration);
    }

}
