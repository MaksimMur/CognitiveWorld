using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ContinentController : MonoBehaviour
{
    public static bool HaveBeenPressed { get; set; } = false;
    [SerializeField]
    private Animator _continetUIAnimator;
    [SerializeField]
    private ContinentInfo _continentInfo;


    [Header("Continent info")]
    [SerializeField]
    private Image _continentImage;
    [SerializeField]
    private Text _continentName;
    [SerializeField]
    private Continent continentName;
    public void PressContinentButton()
    {
        if (HaveBeenPressed) return;
        HaveBeenPressed = true;
        print("da");
        this.GetComponent<Animator>().Play("Pressed");
    }
    public void HighlightedContinentButton()
    {
        if (HaveBeenPressed) return;
        this.GetComponent<Animator>().Play("Highlighted");
    }

    public void OnButtonPressed()
    {
        HaveBeenPressed = false;
    }

    private static bool openContinentsStart = false;
    private static bool backToMenu = false;
    public void BackToMenu()
    {
        if (!backToMenu) return;
        CanvasController.S.GetComponent<Animator>().Play("OpenMainMenu");
        backToMenu = !backToMenu;
    }

    public void OpenContinentInfo()
    {
        if (openContinentsStart || continentName == Continent.None) return;
        _continetUIAnimator.SetTrigger("OpenContinentInfo");
        openContinentsStart = true;
        _continentInfo.SetContinentInfoUI(_continentImage.sprite, _continentName.text, continentName);
    }

    public void CloseContinentInfo()
    {
        if (!openContinentsStart) return;
        _continetUIAnimator.SetTrigger("OpenContinents");
        openContinentsStart = false;
    }

    public void HaveOpenedContinent()
    {
        openContinentsStart = false;
    }

    public static void ResetData()
    {
        openContinentsStart = false;
        backToMenu = false;
    }
}
