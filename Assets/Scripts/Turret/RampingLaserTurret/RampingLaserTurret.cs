using UnityEngine;
using System.Collections;

public class RampingLaserTurret : ILaserTurret
{
    private float startingDps = 20f;
    private float dpsGainPerSec = 40f;

    override protected void TargetLost() {
        dps = startingDps;
    }

    override protected void FireModel()
    {
        dps += dpsGainPerSec * Time.deltaTime;
        base.FireModel();
    }

    override protected void FireGUI()
    {
        float p = Mathf.Clamp((dps - startingDps) / (2 * dpsGainPerSec), 0, 1);
        Color laserColor = (1 - p) * Color.yellow + p * Color.green;
        laser.startColor = laserColor;
        laser.endColor = laserColor;
        laserEffect.GetComponent<Renderer>().material.color = laserColor;
        lightEffect.GetComponent<Light>().color = laserColor;
        base.FireGUI();
    }
}
