using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    private static float startLife = 100f;
    private float currentLife = startLife;
    private Enemy enemy;

    public GameObject healthBar;
    public GameObject health;

    public float damageMultiplier = 1;

    void Start()
    {
        currentLife = startLife;
        enemy = GetComponent<Enemy>();
    }

    public void Damage(float damage)
    {
        currentLife -= damageMultiplier * damage;
        healthBar.SetActive(true);
        health.transform.localScale = new Vector3(currentLife / startLife, 1, 1);
        if (currentLife <= 0)
        {
            enemy.Die();
        }
    }
}
