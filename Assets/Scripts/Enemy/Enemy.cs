using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int bounty = 50;

    public GameObject deathEffect;

    private EnemyMove movement;

    public float radius = 0.5f;

    private ISet<GameObject> incomingBullets;
    private bool isDead = false;

    private void Start()
    {
        movement = GetComponent<EnemyMove>();
        incomingBullets = new HashSet<GameObject>();
    }

    public void RegisterBullet(GameObject bullet)
    {
        if (incomingBullets == null)
        {
            incomingBullets = new HashSet<GameObject>();
        }
        incomingBullets.Add(bullet);
    }

    public void OnTeleport()
    {
        PlayerManager.playerManager.EnemyEscaped();
        foreach (GameObject bullet in incomingBullets)
        {
            if (bullet != null)
            {
                bullet.GetComponent<IBullet>().TargetTeleport();
            }
        }
        incomingBullets.Clear();
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        UpdateDeathStats();
        TriggerDeathAnimation();
    }

    private void UpdateDeathStats()
    {
        PlayerManager.playerManager.EnemyDeath(bounty);
        WaveManager.waveManager.EnemyDeath();
    }

    private void TriggerDeathAnimation()
    {
        GameObject deathParticles = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticles, 4f);
        Destroy(gameObject);

    }
}
