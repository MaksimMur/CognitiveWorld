using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData : MonoBehaviour
{
    private static ProfileData Instanse;

    public void Awake()
    {
        Instanse = this;
    }
    private void IncreaseInfo(ProfileDataEnum data)
    {
        string strokeData = data.ToString();
        if (PlayerPrefs.HasKey(strokeData))
        {
            PlayerPrefs.SetInt(strokeData, PlayerPrefs.GetInt(strokeData) + 1);
        }
        else
        {
            PlayerPrefs.SetInt(strokeData, 1);
        }
    }

    private void DecreaseInfo(ProfileDataEnum data) 
    {
        string strokeData = data.ToString();
        if (PlayerPrefs.HasKey(strokeData))
        {
            int count = PlayerPrefs.GetInt(strokeData);
            if (count < 1)
            {
                PlayerPrefs.SetInt(strokeData, 0);
                return; 
            }
            PlayerPrefs.SetInt(strokeData, PlayerPrefs.GetInt(strokeData) - 1);
        }
    }

    private void SetDifData(ProfileDataEnum data, bool Increase = true)
    {
        if (Increase)
        {
            IncreaseInfo(data);
            if (data.ToString().Contains("Right") && data != ProfileDataEnum.CountComonRightAnswers)
            {
                IncreaseInfo(ProfileDataEnum.CountComonRightAnswers);
            }
            if (data.ToString().Contains("Lie") && data != ProfileDataEnum.CountComonLieAnswers)
            {
                IncreaseInfo(ProfileDataEnum.CountComonLieAnswers);
            }
        }
        else
        {
            DecreaseInfo(data);
        }
    }

    public static void SetInfo(ProfileDataEnum data, bool Increase = true)
    {
        Instanse.SetDifData(data,Increase);
    }




}

public enum ProfileDataEnum
{ 
None,
CountGame,
CountComonRightAnswers,
CountComonLieAnswers,
CountLearnedCountries,
RightFlags,
LieFlags,
RightCountriesByCapital,
LieCountriesByCapital,
RightCapitalByCountries,
LieCapitalByCountries,
RightGenerationSequence,
LieGenerationSequence,
RightGenerationSquare,
LieGenerationSquare
}
