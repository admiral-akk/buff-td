using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IBullet
{
    public float damage = 10f;

    override protected void Damage()
    {
        base.Damage();
        target.Damage(damage);
        Die();
    }
}
