using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameChooseCountryAndCapital : Game
{
    public ChooseModeGame chooseMode;

    public Timer timer;
    public int GameSecond = 30;
    public Continent continent;
    public ContinentInfo continentInfo;
    public EndPanel endPanel;
    public Text TotalAnswers;

    public Text CurrentObj;

    public AnswerButton[] buttons;

    public List<Country> listCountries;

    public List<Country> GetCountries = new List<Country>();

    public List<Country> AnswerCountries;

    public List<Answer> _listAnswers = new List<Answer>();



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
        if (ChooseModeGame.ChooseCountryByCapitalName == chooseMode)
        {
            TotalAnswers.text = $"¬ы угадали стран по названию столиц: {_listAnswers.Where(x => x.IsRight).Count()}/{_listAnswers.Count}";
        }
        else
        {
            TotalAnswers.text = $"¬ы угадали столиц по названию страны: {_listAnswers.Where(x => x.IsRight).Count()}/{_listAnswers.Count}";
        }
    }

    public override void TryAgainGame()
    {
        IsGameEnd = false;
        GetCountries = new List<Country>();
        _listAnswers = new List<Answer>();
        endPanel.HidePanel();
        InitGame(continent, chooseMode);
    }

    public override void ExitGame()
    {
        continentInfo.OpenContinentInfoFromGame(this.chooseMode);
    }

    public void InitGame(Continent con, ChooseModeGame Mode)
    {
        chooseMode = Mode;
        _listAnswers = new List<Answer>();
        DefaultButtonController.HaveBeenPressed = false;
        GetCountries = new List<Country>();
        listCountries = CountriesAndContinentsInfo.GetContinentCountrisByName(con);
        continent = con;
        InitGameBlock();
        StartGame();
    }

    public override void AnswerClick(AnswerButton answerButton)
    {
        buttonClicked = answerButton;
        StartCoroutine(GetAnswer());

    }


    AnswerButton buttonClicked;
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
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
            }
            GetAnswerStart = !GetAnswerStart;
            yield return new WaitForSeconds(0.5f);
        }
        if (!AnswerResultWait)
        {
            print($"{buttonClicked.country.CountryName} {buttonClicked.country.CountryCapitalName} {RightCountry}");
            if (buttonClicked.country.CountryName == RightCountry || buttonClicked.country.CountryCapitalName == RightCountry)
            {
                _listAnswers.Add(new Answer(true));
                buttonClicked.WinColor();
            }
            else
            {
                buttonClicked.LoseColor();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].country.CountryName == RightCountry || buttons[i].country.CountryCapitalName == RightCountry)
                    {
                        buttons[i].WinColor();
                    }
                }
                _listAnswers.Add(new Answer(false));
            }
            AnswerResultWait = true;
            yield return new WaitForSeconds(0.5f);
        }
        GetAnswerStart = !GetAnswerStart;
        AnswerResultWait = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].enabled = true;
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

    public string RightCountry;

    public void InitGameBlock()
    {
        List<Country> r = listCountries.Where(x => !GetCountries.Contains(x)).ToList();
        if (r.Count == 0)
        {
            EndGame();
            return;
        }
        Country rightCountry = r[Random.Range(0, r.Count)];
        r.Clear();
        GetCountries.Add(rightCountry);
        //выбираем текст дл€ режима
        CurrentObj.text = GetCurrentObjText(rightCountry);

        if (chooseMode == ChooseModeGame.ChooseCountryByCapitalName)
        {
            RightCountry = rightCountry.CountryName;
        }
        else
        {
            RightCountry = rightCountry.CountryCapitalName;
        }

        List<Country> answerCountries = new List<Country>();
        int index = Random.Range(0, buttons.Length);

        buttons[index].DefaultColor();
        buttons[index].country = rightCountry;
        if (chooseMode == ChooseModeGame.ChooseCountryByCapitalName)
        {
            buttons[index].SetCountryCapitalName();
        }
        else
        {
            buttons[index].SetCountryName();
        }


        answerCountries.Add(buttons[index].country);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index) continue;
            buttons[i].DefaultColor();
            int randomIndex = Random.Range(0, listCountries.Count - i - 1);
            //print($"{listCountries.Where(x => !answerCountries.Contains(x)).ToList()}__{randomIndex}");
            buttons[i].country = listCountries.Where(x => !answerCountries.Contains(x)).ToList()[randomIndex];
            if (chooseMode == ChooseModeGame.ChooseCountryByCapitalName)
            {
                buttons[i].SetCountryCapitalName();
            }
            else
            {
                buttons[i].SetCountryName();
            }
            answerCountries.Add(buttons[i].country);
        }
        answerCountries.Clear();
        DefaultButtonController.HaveBeenPressed = false;
    }

    public string GetCurrentObjText(Country c)
    {
        if (ChooseModeGame.ChooseCapitalByCountryName == chooseMode)
        {
            return $"”гадайте страну с названием столицей: {c.CountryCapitalName}";
        }
        else
        {
            return $"”гадайте столицу с названием страны: {c.CountryName}";
        }
    }
}
