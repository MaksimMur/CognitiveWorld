using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController S;

    private void Awake()
    {
        S = this;
    }

    public void OpenContinents()
    {
        ContinentController.ResetData();
    }

    public void OpenMainMenu()
    {
        ContinentController.ResetData();
    }
}
