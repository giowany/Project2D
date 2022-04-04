using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EBAC.Core.Singleton;

public class ItemManager : Singleton<ItemManager> 
{
    [SerializeField] private int _coins;
    public TextMeshProUGUI textCoins;
    public void AddCoins(int amount = 1)
    {
        _coins += amount;
        textCoins.text = "X" + _coins.ToString();
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
