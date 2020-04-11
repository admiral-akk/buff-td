using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : IBullet
{
    private float startSpeed = 5f;
    private float speedGainPerSecond = 15f;

    private void Start()
    {
        speed = startSpeed;
    }

    override protected void Update()
    {
        base.Update();
        speed += speedGainPerSecond * Time.deltaTime;
    }

    override protected void Damage()
    {
        base.Damage();
        target.Damage(2.5f * speed);
        Die();
    }

    override public void TargetTeleport()
    {
        if (!FindNewTarget())
            Die();
    }

    override protected void TargetNull()
    {
        if (!FindNewTarget())
            Die();
    }

    private bool FindNewTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                if (target == null ||
                    Vector3.Distance(transform.position, collider.transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
target = collider.gameObject.GetComponent<EnemyLife>();
                }
            }
        }
        return target != null;
    }
}
