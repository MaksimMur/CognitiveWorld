using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Game game;

    public bool isPlaying = false;
    public bool isEnd = false;
    public int amountSecond = 0;
    public float StartPlaying = 0;


    public void PlayTimer(int seconds)
    {
        isPlaying = true;
        isEnd = false;
        StartPlaying = Time.time;
        amountSecond = seconds;
    }

    public void Update()
    {
        if (isPlaying)
        { 
            float u= (Time.time - StartPlaying) / amountSecond;
            if (u >= 1)
            {
                isPlaying = false;
                isEnd = true;
                game.EndGame();
            }
            timerText.text = Mathf.Round(amountSecond - (u * amountSecond)).ToString();
        }
    }
}
