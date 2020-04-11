using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProjectileTurret : ITurret
{
    protected float fireRate = 1f;
    public GameObject bulletPrefab;

    protected float countdownToFire = 0.0f;

    virtual protected void Update()
    {
        countdownToFire -= Time.deltaTime;
        UpdateTargetAndFire();
    }

    override protected void Fire()
    {
        countdownToFire = 1 / fireRate;
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletObject.GetComponent<IBullet>().Fire(target);
        target.gameObject.GetComponent<Enemy>().RegisterBullet(bulletObject);
    }

    override protected bool CanFire()
    {
        return countdownToFire <= 0;
    }
}
