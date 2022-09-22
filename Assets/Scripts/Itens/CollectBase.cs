using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBase : MonoBehaviour
{

    public string tagPlayer = "Player";
    public GameObject render;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var c = GetComponent<Collider2D>();

        if (collision.transform.CompareTag(tagPlayer))
        {
            Collect();
            c.enabled = false;
        }
    }

    protected virtual void Collect()
    {
        render.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        
    }
}
