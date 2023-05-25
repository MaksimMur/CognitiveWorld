using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

class CompareCounriesByGeneration : IComparer<Country>
{
    public int Compare(Country? p1, Country? p2)
    {
        if (p1 is null || p2 is null)
            throw new ArgumentException("Некорректное значение параметра");
        return Convert.ToInt32(string.Concat(p1.Generation.Where(x => char.IsDigit(x)).ToList())) - Convert.ToInt32(string.Concat(p2.Generation.Where(x => char.IsDigit(x)).ToList()));
    }
}

class CompareCounriesBySquare : IComparer<Country>
{
    public int Compare(Country? p1, Country? p2)
    {
        if (p1 is null || p2 is null)
            throw new ArgumentException("Некорректное значение параметра");
        return Convert.ToInt32(string.Concat(p1.Square.Where(x => char.IsDigit(x)).ToList())) - Convert.ToInt32(string.Concat(p2.Square.Where(x => char.IsDigit(x)).ToList()));
    }
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
