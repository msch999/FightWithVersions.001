﻿using UnityEngine;
using UnityEngine.UI;



public class PancakeScoreBoardController : MonoBehaviour
{

    public Text timerText;
    public Text numPancakesText;
    public PancakeManager pancakeManager;

    public float timeLeft;
    private bool inGame;
    private int numPancakes;

    void Start()
    {
        inGame = false;
        numPancakes = 0;
    }

    void Update()
    {
        if (inGame)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + (int)timeLeft;
            if (timeLeft <= 0)
            {
                GameOver();
            }
        }
    }

    public void StartGame()
    {
        inGame = true;
        numPancakes = 0;
        if (timeLeft == 0)
            timeLeft = 30f;
        
        numPancakesText.text = "Flipped Pancakes: " + numPancakes;
    }

    private void GameOver()
    {
        inGame = false;
        timerText.text = "Game Over";
        pancakeManager.GameOver();
    }

    public void PancakeFlipped()
    {
        ++numPancakes;
        numPancakesText.text = "Flipped Pancakes: " + numPancakes;
    }
}
