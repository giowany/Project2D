using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;

public class ItemManager : Singleton<ItemManager> 
{
    [SerializeField] private int _coins;

    public void AddCoins(int amount = 1)
    {
        _coins += amount;
    }

    private void ResetItens()
    {
        _coins = 0;
    }

    private void Start()
    {
        ResetItens();
    }
}
