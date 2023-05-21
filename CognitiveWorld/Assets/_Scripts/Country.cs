using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Country
{
    public string Key;
    public string Continent;
    public string Square;
    public string Generation;
    public string CountryName;
    public string CountryCapitalName;
    public string Currency;
    public string PoliticalForm;
    public string Language;
    public string Religion;
    public string Facts;
    public string LinkOnWikipedia;

    public Sprite Flag { get; set; }
}

public enum Continent
{ 
Planet,
Europe,
Asia,
Africa,
SouthAmerica,
NorthAmerica,
Oceania,
None
}
