using System;
using UnityEngine;

public class MarkerDebuff : MonoBehaviour
{
    private EnemyLife life;
    private EnemyMove move;
    public float timeLeft = 0.3f;

    private void Start()
    {
        life = GetComponent<EnemyLife>();
        move = GetComponent<EnemyMove>();

        life.damageMultiplier = 2;
        move.moveMultiplier = 0.60f;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            Remove();
    }

    public void Remove()
    {
        life.damageMultiplier = 1;
        move.moveMultiplier = 1;
        Destroy(this);
    }
}
