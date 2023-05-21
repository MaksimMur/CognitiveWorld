using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text te;
    public Country country;

    public Game game;

    public void SetCountryName(Country c=null)
    {
        if (country == null && c == null)
        {
            return;
        }
        if (c != null) 
        {
            country = c;
            te.text = c.CountryName;
            return;
        }
        if (country != null)
        {
            te.text = country.CountryName;
            return;
        }
    }

    public void AnswerClick()
    {
        game.AnswerClick(this);
    }
}
