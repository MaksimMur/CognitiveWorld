using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultButtonController : MonoBehaviour
{
    public static bool HaveBeenPressed { get; set; } = false;
    public void Pressed()
    {
        if (HaveBeenPressed) return;
        HaveBeenPressed = true;
        this.GetComponent<Animator>().Play("Pressed");
    }

    public void Highlighted()
    {
        if (HaveBeenPressed) return;
        this.GetComponent<Animator>().Play("Highlighted");
    }

    public void OnPressedEnd()
    {
        HaveBeenPressed = false;
    }
}
