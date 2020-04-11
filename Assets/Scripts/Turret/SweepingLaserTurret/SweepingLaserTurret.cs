using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLaserTurret : ITurret
{
    private float dps = 250f;
    private float aoeRange = 0.6f;
    protected float fireRate = 0.4f;
    private Vector3 targetPosition;
    private Vector3 sweepDirection;
    private float sweepLength = 2.5f;
    private float fireAnimationTime = 0.0f;
    private float fireAnimationLength = 0.2f;
    private bool isFiring = false;

    public LineRenderer laser;
    public ParticleSystem laserEffect;
    public GameObject lightEffect;

    private float countdownToFire = 0.0f;

    private void Start()
    {
        DisableLaser();
    }

    void Update()
    {
        countdownToFire -= Time.deltaTime;
        fireAnimationTime += Time.deltaTime;
        UpdateTargetAndFire();
        if (isFiring)
        {
            UpdateLaser();
        } 
    }

    private void UpdateLaser()
    {
        if (fireAnimationTime > fireAnimationLength)
        {
            isFiring = false;
            DisableLaser();
            return;
        }

        Vector3 laserPosition = targetPosition + sweepDirection * sweepLength * (fireAnimationTime - fireAnimationLength / 2) / fireAnimationLength;

        laser.SetPosition(0, transform.position + new Vector3(0, 0.2f, 0));
        laser.SetPosition(1, laserPosition);
        laserEffect.transform.position = laserPosition;
        laserEffect.transform.LookAt(laserEffect.transform.position + new Vector3(0, 1, 0));
        DealDamage();
    }

    private void DealDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(laserEffect.transform.position, aoeRange);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyLife>().Damage(dps * Time.deltaTime);
            }
        }
    }

    private void DisableLaser()
    {
        laser.enabled = false;
        laserEffect.Stop();
        lightEffect.SetActive(false);
    }

    private void SweepLaser()
    {
        fireAnimationTime = 0f;
        isFiring = true;
        targetPosition = target.transform.position;
        targetPosition.y = transform.position.y + 0.05f;

        sweepDirection = Vector3.Cross(targetPosition - transform.position, new Vector3(0, 1,0)).normalized;

        laser.enabled = true;
        laserEffect.Play();
        lightEffect.SetActive(true);
    }

    override protected void Fire()
    {
        countdownToFire = 1 / fireRate + fireAnimationLength;
        SweepLaser();
    }

    override protected bool CanFire()
    {
        return countdownToFire <= 0;
    }
}
