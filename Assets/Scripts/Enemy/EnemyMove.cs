using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Enemy enemy;

    public float moveMultiplier = 1f;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }


    void Update()
    {
        Vector3 dir = GameManager.gameManager.GetEndNodePosition() - transform.position;

        if (dir.magnitude < 0.5f)
        {
            transform.position = GameManager.gameManager.GetStartNodePosition();
            enemy.OnTeleport();
        }
        else
        {
            transform.Translate(dir.normalized * moveSpeed * moveMultiplier * Time.deltaTime);
        }
    }
    }