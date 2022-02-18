using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : CollectBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.instance.AddCoins();
    }
}
