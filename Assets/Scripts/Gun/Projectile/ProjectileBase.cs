using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;
    public float lifeTime = 2f;
    public float side = 1f;

    protected virtual void Foward()
    {
        transform.Translate(direction * side * Time.deltaTime);
    }

    void Update()
    {
        Foward();
    }

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }
}
