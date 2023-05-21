using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryViewBlock : MonoBehaviour
{
    public Text _countryName;

    public Image _flag;

    public void SetInfo(string countryName, Sprite flag)
    {
        _countryName.text= countryName;
        _flag.sprite = flag;
    }

}
