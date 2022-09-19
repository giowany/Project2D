using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierProtection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var g = collision.GetComponent<BulletBase>();
        if(g != null)
        {
            Destroy(g);
        }

        var p = collision.GetComponent<PlayerController>();
        if(p != null)
        {
            Destroy(gameObject);
        }
    }
}
