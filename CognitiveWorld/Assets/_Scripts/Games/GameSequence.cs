using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameSequence : Game
{
    public ChooseModeGame chooseMode;

    public Timer timer;
    public int GameSecond = 30;
    public Continent continent;
    public ContinentInfo continentInfo;
    public EndPanel endPanel;
    public Text TotalAnswers;
    public Text CurentObjText;

    public List<Country> listCountries;

    public List<Answer> _listAnswers = new List<Answer>();

    public CountrySlot startSlotTransform;
    public List<DraggableItem> countriesDrugableList;
    public List<CountrySlot> countriesSlotsList;


    public override void StartGame()
    {
        IsGameEnd = false;
        timer.PlayTimer(GameSecond);
        base.StartGame();
    }

    public override void EndGame()
    {
        IsGameEnd = true;
        timer.StopAllCoroutines();
        StopAllCoroutines();
        endPanel.ShowPanel();
        if (ChooseModeGame.SequenceGeneration == chooseMode)
        {
            TotalAnswers.text = $"Правильно упорядоченых стран по количеству населенеия: {_listAnswers.Where(x => x.IsRight).Count()}/{_listAnswers.Count}";
        }
        else
        {
            TotalAnswers.text = $"Правильно упорядоченых стран по площади территории: {_listAnswers.Where(x => x.IsRight).Count()}/{_listAnswers.Count}";
        }
    }

    public override void TryAgainGame()
    {
        IsGameEnd = false;
        endPanel.HidePanel();
        InitGame(continent, chooseMode);
    }

    public override void ExitGame()
    {
        continentInfo.OpenContinentInfoFromGame(this.chooseMode);
    }
    List<Country> rightSequenceList= new List<Country>();
    public void InitGameBlock()
    {
        rightSequenceList = new List<Country>();
        for (int i = 0; i < countriesSlotsList.Count; i++)
        {
            countriesSlotsList[i].SetDefault();
            countriesSlotsList[i].SetActiveAnswer(false);
        }
        List<Country> counriesForItems = new List<Country>();
        for (int i = 0; i < countriesDrugableList.Count; i++)
        {
            // print(listCountries.Where(x => !counriesForItems.Contains(x)).ToList()[Random.Range(0, listCountries.Count - 1 - i)]);
            counriesForItems.Add(listCountries.Where(x => !counriesForItems.Contains(x)).ToList()[Random.Range(0, listCountries.Count - 1 - i)]);
            countriesDrugableList[i].SetCountry(counriesForItems[i]);
            rightSequenceList.Add(counriesForItems[i]);
            countriesDrugableList[i].transform.SetParent(startSlotTransform.slotTransform);
        }

        if (ChooseModeGame.SequenceGeneration == chooseMode)
        {
            rightSequenceList.Sort(new CompareCounriesByGeneration());
        }
        else
        {
            rightSequenceList.Sort(new CompareCounriesBySquare());
        }

        //for (int i = 0; i < 4; i++)
        //{
        //    print($"{countriesDrugableList[i].country.CountryName} / {rightSequenceList[i].CountryName}");
        //}
    }


    public void InitGame(Continent con, ChooseModeGame Mode)
    {
        chooseMode = Mode;
        _listAnswers = new List<Answer>();
        DefaultButtonController.HaveBeenPressed = false;
        listCountries = CountriesAndContinentsInfo.GetContinentCountrisByName(con);
        CurentObjText.text = GetCurrentObjText();
        //Debug.Log($"{listCountries.Count}");
        continent = con;
        InitGameBlock();
        StartGame();
    }

    public void GetAnswerClick()
    {
        StartCoroutine(GetAnswer());
    }

    bool GetAnswerStart = false;
    bool AnswerResultWait = false;
    public IEnumerator GetAnswer()
    {
        if (IsGameEnd)
        {
            StopCoroutine(GetAnswer());
            yield return null;
        }
        if (!GetAnswerStart)
        {
            for (int i = 0; i < countriesDrugableList.Count; i++)
            {
                countriesDrugableList[i].img.raycastTarget = false;
            }
            GetAnswerStart = !GetAnswerStart;
            yield return new WaitForSeconds(0.5f);
        }
        if (!AnswerResultWait)
        {
            //get asnwer
            bool answer=true;
            if (countriesSlotsList.Count(x => x.slotTransform.childCount == 1) < countriesSlotsList.Count)
            {
                answer = false;
            }
            else
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    print($"{countriesSlotsList[i].country.CountryName} --- {rightSequenceList[i].CountryName}");
                //}
                for (int i = 0; i < rightSequenceList.Count; i++)
                {
                    if (rightSequenceList[i].CountryName != countriesSlotsList[i].country.CountryName)
                        answer = false;
                }
            }
            // get answer response
            if (answer)
            {
                _listAnswers.Add(new Answer(true));
            }
            else
            {
                _listAnswers.Add(new Answer(false));
            }
            //get answer interface
            for (int i = 0; i < countriesSlotsList.Count; i++)
            {       
                if (countriesSlotsList[i].country!= null && rightSequenceList[i].CountryName == countriesSlotsList[i].country.CountryName)
                {
                    countriesSlotsList[i].SetRight();
                }
                else
                {
                    countriesSlotsList[i].SetLie();
                }
                countriesSlotsList[i].SetActiveAnswer(true, chooseMode);
            }

            AnswerResultWait = true;
            yield return new WaitForSeconds(1.5f);
        }
        GetAnswerStart = !GetAnswerStart;
        AnswerResultWait = false;
        for (int i = 0; i < countriesDrugableList.Count; i++)
        {
            countriesDrugableList[i].img.raycastTarget = true;
        }
        InitGameBlock();
        StopAllCoroutines();
    }

    public void RightAnswer()
    {

    }

    public void UnCorrectAnswer()
    {

    }
    public string GetCurrentObjText()
    {
        if (ChooseModeGame.SequenceGeneration == chooseMode)
        {
            return $"Упорядочить страны по количеству населенеия";
        }
        else
        {
            return $"Упорядочить страны по площади территории";
        }
    }
}

