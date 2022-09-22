using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashDamage : MonoBehaviour
{
    public List<SpriteRenderer> renderers;
    public Color flashDamage;
    public float duration = .3f;

    private void OnValidate()
    {
        renderers = new List<SpriteRenderer>();
        foreach(var sprites in GetComponentsInChildren<SpriteRenderer>())
        {
            renderers.Add(sprites);
        }
    }

    public void Flash()
    {
        foreach(var s in renderers)
        {
            if (s.color.a != 1) return;
            s.DOColor(flashDamage, duration).SetLoops(4, LoopType.Yoyo);
        }
    }
}