using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : ITurret
{
    public float dps = 30f;

    public LineRenderer laser;
    public ParticleSystem laserEffect;
    public GameObject lightEffect;

    void LateUpdate()
    {
        UpdateTargetAndFire();
    }

    override protected void UpdateNoTarget()
    {
        lightEffect.SetActive(false);
        laser.enabled = false;
        if (laserEffect.isPlaying)
            laserEffect.Pause();
    }

    override protected void Fire()
    {
        lightEffect.SetActive(true);

        laser.enabled = true;
        laser.SetPosition(0, transform.position + new Vector3(0, 0.2f, 0));
        laser.SetPosition(1, target.transform.position);

        Vector3 dir = transform.position - target.transform.position;

        laserEffect.transform.position = target.transform.position + 0.5f * dir.normalized;
        laserEffect.transform.LookAt(dir);
        if (!laserEffect.isPlaying)
            laserEffect.Play();
        target.Damage(dps * Time.deltaTime);
    }
}
