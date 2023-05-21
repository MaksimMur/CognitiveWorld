using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowPanel()
    {
        anim.ResetTrigger("Hide");
        anim.SetTrigger("Show");
    }

    public void HidePanel()
    {
        anim.ResetTrigger("Show");
        anim.SetTrigger("Hide");
    }
}

