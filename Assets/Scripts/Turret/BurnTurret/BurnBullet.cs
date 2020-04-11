using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBullet : IBullet
{
    override protected void Damage()
    {
        base.Damage();
        target.gameObject.AddComponent(typeof(BurnEffect));
        Die();
    }
}
