using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;

public class ItemManager : Singleton<ItemManager> 
{
    [SerializeField] private int coins;

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }

    private void ResetItens()
    {
        coins = 0;
    }

    private void Start()
    {
        ResetItens();
    }
}
