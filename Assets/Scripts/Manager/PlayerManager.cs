using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;

    private static int startMoney = 500;
    private int currentMoney;
    private static int startLives = 20;
    private int currentLives;

    void Start()
    {
        if (playerManager != null)
        {
            Debug.Log("MULTIPLE PLAYER MANAGERS!!!");
            return;
        }
        playerManager = this;
        currentMoney = startMoney;
        currentLives = startLives;
    }

    public void EnemyDeath(int bounty)
    {
        currentMoney += bounty;
    }

    public bool CanAfford(int cost)
    {
        return currentMoney >= cost;
    }


    public void TurretBuilt(int cost)
    {
        currentMoney -= cost;
    }

    public void EnemyEscaped()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            GameManager.gameManager.GameOver();
        }
    }

    public int GetLives()
    {
        return currentLives;
    }

    public int GetMoney()
    {
        return currentMoney;
    }
}
