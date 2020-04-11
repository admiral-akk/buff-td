using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public GameObject canvas;

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false);
    }
}
