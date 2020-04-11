using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : IProjectileTurret
{
    public GameObject chargeBar;
    public GameObject charge;
    public LineRenderer laser;

    private float chargeTime = 1.3f;

    private void Start()
    {
        range = Mathf.Infinity;
        fireRate = 0.5f;
    }

    void LateUpdate()
    {
        charge.transform.localScale = new Vector3((chargeTime - countdownToFire) / chargeTime, 1, 1);
        if (target != null)
        {
            laser.SetPosition(0, transform.position + new Vector3(0, 0.2f, 0));
            laser.SetPosition(1, target.transform.position);
        }
    }

    override protected void TargetLost()
    {
        countdownToFire = chargeTime;
        chargeBar.SetActive(false);
        laser.enabled = false;
    }

    override protected void TargetFound()
    {
        chargeBar.SetActive(true);
        laser.enabled = true;
    }

    override protected void UpdateNoTarget()
    {
        countdownToFire = chargeTime;
    }

    override protected void Fire()
    {
        base.Fire();
        target = null;
    }
}
