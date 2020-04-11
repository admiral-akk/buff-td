using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTurret : IProjectileTurret
{
    override protected void Fire()
    {
        base.Fire();
        target = null;
    }
}

