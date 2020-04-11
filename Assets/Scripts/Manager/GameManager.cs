using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager gameManager;

    public bool isGameOver = false;

    [Header("Pathing Variables")]
    public GameObject startNode;
    public GameObject endNode;

    [Header("UI Variables")]
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    void Start()
    {
        if (gameManager != null)
        {
            Debug.Log("MULTIPLE GAME MANAGERS!!!");
            return;
        }
        gameManager = this;

    }

    public Vector3 GetStartNodePosition()
    {
        return startNode.transform.position;
    }

    public Vector3 GetEndNodePosition()
    {
        return endNode.transform.position;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        isGameOver = true;
        gameWinUI.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
