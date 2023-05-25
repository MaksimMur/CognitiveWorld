using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameChooseFlag : Game
{
    public Timer timer;
    public int GameSecond = 30;
    public Continent continent;
    public Image flag;
    public ContinentInfo continentInfo;
    public EndPanel endPanel;
    public Text TotalAnswers;

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
        TotalAnswers.text = $"Вы угадали флагов: {_listAnswers.Where(x=>x.IsRight).Count()}/{_listAnswers.Count}";
    }

    public override void TryAgainGame()
    {
        IsGameEnd = false;
        _listAnswers = new List<Answer>();
        GetCountries = new List<Country>();
        endPanel.HidePanel();
        InitGame(continent);
    }

    public override void ExitGame()
    {
        continentInfo.OpenContinentInfoFromGame(ChooseModeGame.Flags);
    }

    public void InitGame(Continent con)
    {
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
            if (buttonClicked.country.CountryName == RightCountry)
            {
                _listAnswers.Add(new Answer(true));
                buttonClicked.WinColor();
            }
            else
            {
                buttonClicked.LoseColor();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].country.CountryName == RightCountry)
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
        flag.sprite = rightCountry.Flag;
        RightCountry = rightCountry.CountryName;
        List<Country> answerCountries = new List<Country>();
        int index = Random.Range(0, buttons.Length);

        buttons[index].DefaultColor();
        buttons[index].country = rightCountry;
        buttons[index].SetCountryName();

        answerCountries.Add(buttons[index].country);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index) continue;
            buttons[i].DefaultColor();
            int randomIndex = Random.Range(0, listCountries.Count - i - 1);
            //print($"{listCountries.Where(x => !answerCountries.Contains(x)).ToList()}__{randomIndex}");
            buttons[i].country = listCountries.Where(x => !answerCountries.Contains(x)).ToList()[randomIndex];
            buttons[i].SetCountryName();
            answerCountries.Add(buttons[i].country);
        }
        answerCountries.Clear();
        DefaultButtonController.HaveBeenPressed = false;
    }
}
