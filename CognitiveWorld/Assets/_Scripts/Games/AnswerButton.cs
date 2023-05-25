using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text te;
    public Country country;

    public Color winColor, loseColor, defaultColor;
    public Color WinColor()=> this.gameObject.GetComponent<Image>().color = winColor; 
    public Color LoseColor() => this.gameObject.GetComponent<Image>().color = loseColor; 
    public Color DefaultColor() => this.gameObject.GetComponent<Image>().color = defaultColor; 



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

    public void SetCountryCapitalName(Country c = null)
    {
        if (country == null && c == null)
        {
            return;
        }
        if (c != null)
        {
            country = c;
            te.text = c.CountryCapitalName;
            return;
        }
        if (country != null)
        {
            te.text = country.CountryCapitalName;
            return;
        }
    }

    public void AnswerClick()
    {
        game.AnswerClick(this);
    }
}
