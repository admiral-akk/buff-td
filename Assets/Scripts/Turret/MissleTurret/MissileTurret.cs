using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : IProjectileTurret
{
    private void Start()
    {
        range = Mathf.Infinity;
        fireRate = 0.5f;
    }
}
