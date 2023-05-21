using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountryInformationSetter : MonoBehaviour
{
    public TextAsset countriesJson;
    public Sprite[] textureCountrySprites;

    [System.Serializable]
    public class CountryInfo
    {
        public Country[] countries;
    }

    public CountryInfo countryList = new CountryInfo();
    private List<Sprite> countrySprites  = new List<Sprite>();

    public void Start()
    {
        countryList = JsonUtility.FromJson<CountryInfo>(countriesJson.text);
        countrySprites = textureCountrySprites.ToList();

        for (int i = 0; i < countryList.countries.Length; i++)
        {
            countryList.countries[i].Flag = countrySprites.Where(x => x.name == countryList.countries[i].Key).First();
            print(countryList.countries[i].Flag.name);
        }
        CountriesAndContinentsInfo.SetAllCountries(countryList.countries.ToList());
    }

 

}
