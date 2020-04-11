using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ILaserTurret : ITurret
{
    protected float dps = 20f;

    public LineRenderer laser;
    public ParticleSystem laserEffect;
    public GameObject lightEffect;

    void LateUpdate()
    {
        UpdateTargetAndFire();
    }

    private void UpdateGUINoTarget()
    {
        lightEffect.SetActive(false);
        laser.enabled = false;
        if (laserEffect.isPlaying)
            laserEffect.Pause();
    }

    virtual protected void UpdateModelNoTarget() { }

    override protected void UpdateNoTarget()
    {
        UpdateGUINoTarget();
        UpdateModelNoTarget();
    }

    virtual protected void FireModel()
    {
        target.Damage(dps * Time.deltaTime);
    }

    virtual protected void FireGUI()
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
    }

    override protected void Fire()
    {
        FireGUI();
        FireModel();
    }

}
