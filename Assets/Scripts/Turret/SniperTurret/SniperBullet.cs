using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : IBullet
{
    public float damage = 100f;

    void Start()
    {
        speed = 60f;
    }

    override protected void Damage()
    {
        base.Damage();
        target.Damage(damage);
        Die();
    }
}
