using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class CountriesInfoBlock : MonoBehaviour
{
    public Transform countriesPanel;
    public List<Country> countriesList;
    public CountryViewBlock countryPrefab;
    public Continent continent=Continent.None;
    public void SetCountries(Continent cont)
    {
        if (continent == cont) return;
        continent = cont;
        List<CountryViewBlock> countries = countriesPanel.GetComponentsInChildren<CountryViewBlock>().ToList();
        for (int i = 0; i < countries.Count; i++)
        {
            Destroy(countries[i].gameObject);        
        }

        countriesList = CountriesAndContinentsInfo.GetContinentCountrisByName(continent);
        GetCountriesListPanel(countriesList);
    }
    public void GetCountriesListPanel(List<Country> countries)
    {
        for (int i = 0; i < countries.Count; i++)
        {
            CountryViewBlock g = Instantiate(countryPrefab, countriesPanel, false);
            print(countries[i].Flag.name);
            g.SetInfo(countries[i].CountryName, countries[i].Flag);
        }

    }
}
