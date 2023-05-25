using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour, iGame
{
    public bool IsGameEnd { get; set; }
    public virtual void StartGame()
    {

    }

    public virtual void EndGame()
    {

    }
    public virtual void AnswerClick(AnswerButton answerButton)
    { 
    
    }
    public virtual void ExitGame()
    { 
    
    }
    public virtual void TryAgainGame()
    { 
    
    }
}

public struct Answer
{ 
    public bool IsRight;
    public Answer(bool b) => IsRight = b;
}

public interface iGame
{
    public bool IsGameEnd { get; set; }
    public void StartGame();
    public void EndGame();
    public void AnswerClick(AnswerButton answerButton);
    public void ExitGame();
    public void TryAgainGame();
}

public enum ChooseModeGame
{
    None,
    Flags,
    ChooseCountryByCapitalName,
    ChooseCapitalByCountryName,
    SequenceGeneration,
    SequenceSquare
}