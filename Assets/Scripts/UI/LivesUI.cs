using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;

    void Update()
    {
        livesText.text = "Lives: " + Mathf.Clamp(PlayerManager.playerManager.GetLives(), 0, Mathf.Infinity);
    }
}
