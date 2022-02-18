using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBase : MonoBehaviour
{

    public string tagPlayer = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagPlayer))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {

    }
}
