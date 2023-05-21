using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CountriesAndContinentsInfo : MonoBehaviour
{
    public static List<Country> All_Countries { private set; get; }
    public static List<Country> Europe_Countries { private set; get; }
    public static List<Country> Asia_Countries { private set; get; }
    public static List<Country> SouthAmerica_Countries { private set; get; }
    public static List<Country> NorthAmerica_Countries { private set; get; }
    public static List<Country> Oceania_Countries { private set; get; }
    public static List<Country> Africa_Countries { private set; get; }
    public static async void SetAllCountries(List<Country> countries)
    { 
        All_Countries = countries;
        await Task.Delay(100);
        SetEuropeCountries(countries);
        await Task.Delay(100);
        SetAsiaCountries(countries);
        await Task.Delay(100);
        SetSouthAmericaCountries(countries);
        await Task.Delay(100);
        SetNorthAmericaCountries(countries);
        await Task.Delay(100);
        SetAfricaCountries(countries);
        await Task.Delay(100);
        SetOceaniaCountries(countries);
    }

    private static void SetEuropeCountries(List<Country> countries)
    {
        Europe_Countries = countries.Where(x=>x.Continent=="Европа"|| x.Continent=="Europe").ToList();
    }

    private static void SetAsiaCountries(List<Country> countries)
    {
        Asia_Countries = countries.Where(x => x.Continent == "Азия" || x.Continent == "Asia").ToList();
    }

    private static void SetSouthAmericaCountries(List<Country> countries)
    {
        SouthAmerica_Countries = countries.Where(x => x.Continent == "Южная Америка" || x.Continent == "South America").ToList();
    }
    private static void SetNorthAmericaCountries(List<Country> countries)
    {
        NorthAmerica_Countries = countries.Where(x => x.Continent == "Северная Америка" || x.Continent == "Южная America").ToList();
    }
    private static void SetOceaniaCountries(List<Country> countries)
    {
        Oceania_Countries = countries.Where(x => x.Continent == "Океания" || x.Continent == "Oceania").ToList();
    }
    private static void SetAfricaCountries(List<Country> countries)
    {
        Africa_Countries = countries.Where(x => x.Continent == "Африка" || x.Continent == "Africa").ToList();
    }

    public static List<Country> GetContinentCountrisByName(Continent continent)
    {
        switch (continent)
        { 
            case Continent.Planet: return All_Countries;
            case Continent.Europe: return Europe_Countries;
            case Continent.NorthAmerica: return NorthAmerica_Countries;
            case Continent.SouthAmerica: return SouthAmerica_Countries;
            case Continent.Asia: return Asia_Countries;
            case Continent.Africa: return Africa_Countries;            
            case Continent.Oceania: return Oceania_Countries;
        }
        return null;
    }
}
