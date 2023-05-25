using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContinentInfo : MonoBehaviour
{
    [SerializeField] private Image _continent;
    [SerializeField] private Text _continentText;

    [SerializeField] GameObject ContinentMiniInfo;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject countriesInfo;

    #region Games
    [SerializeField] GameObject GameChooceCountryByFlag;
    [SerializeField] GameObject GameChooceCountryOrCapital;
    [SerializeField] GameObject GameSequence;
    #endregion
    private Continent _continentN;
    public void SetContinentInfoUI(Sprite s, string continentName, Continent continent)
    {
        _continent.sprite = s;
        _continentText.text = continentName;
        _continentN = continent;
    }
    public void OpenCountry()
    {
        ContinentMiniInfo.SetActive(false);
        Menu.SetActive(false);
        countriesInfo.SetActive(true);
        countriesInfo.GetComponent<CountriesInfoBlock>().SetCountries(_continentN);
    }

    public void OpenGameByMode(int modeIndex)
    {
        ChooseModeGame mode = (ChooseModeGame)modeIndex;
        ContinentMiniInfo.SetActive(false);
        Menu.SetActive(false); 
        switch (mode)
        {
            case ChooseModeGame.Flags:
                GameChooceCountryByFlag.SetActive(true);
                GameChooceCountryByFlag.GetComponent<GameChooseFlag>().InitGame(_continentN);
                break;
            case ChooseModeGame.ChooseCapitalByCountryName:
            case ChooseModeGame.ChooseCountryByCapitalName:
                GameChooceCountryOrCapital.SetActive(true);
                GameChooceCountryOrCapital.GetComponent<GameChooseCountryAndCapital>().InitGame(_continentN, mode);
                break;
            case ChooseModeGame.SequenceGeneration:
            case ChooseModeGame.SequenceSquare:
                GameSequence.SetActive(true);
                GameSequence.GetComponent<GameSequence>().InitGame(_continentN, mode);
                break;
        }      
    }


    public void OpenContinentInfoFromGame(ChooseModeGame mode = ChooseModeGame.None)
    {
        ContinentMiniInfo.SetActive(true);
        Menu.SetActive(true);
        GameChooceCountryByFlag.SetActive(false);
        countriesInfo.SetActive(false);

        switch (mode)
        {
            case ChooseModeGame.Flags:
                GameChooceCountryByFlag.SetActive(false);
                break;
            case ChooseModeGame.ChooseCapitalByCountryName:
            case ChooseModeGame.ChooseCountryByCapitalName:
                GameChooceCountryOrCapital.SetActive(false);
                break;
            case ChooseModeGame.SequenceGeneration:
            case ChooseModeGame.SequenceSquare:
                GameSequence.SetActive(false);
                break;
        }
    }
}
