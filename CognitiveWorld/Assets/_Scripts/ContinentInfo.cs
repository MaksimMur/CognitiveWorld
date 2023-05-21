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
}
