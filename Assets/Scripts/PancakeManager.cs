using System.Collections.Generic;
using UnityEngine;


public class PancakeManager : MonoBehaviour
{

    public GameObject pancakePrefab;
    public Transform pancakeDispensePos;
    public PancakeScoreBoardController scoreboard;

    private bool inGame;
    private List<GameObject> pancakes;

    void Start()
    {
        inGame = false;
        pancakes = new List<GameObject>();
    }

    void Update()
    {   // old to new Daydream API
        //if (GvrController.ClickButtonDown && inGame)
        if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton) && inGame)
        {
            AddPancake();
        }
        else if (GvrControllerInput.GetDevice(GvrControllerHand.Dominant).GetButtonDown(GvrControllerButton.TouchPadButton) && !inGame)
        {
            Startgame();
            AddPancake();
        }
    }

    private void AddPancake()
    {
        GameObject pancake = Instantiate(pancakePrefab);
        pancake.transform.position = pancakeDispensePos.position;
        pancakes.Add(pancake);
    }

    public void Startgame()
    {
        inGame = true;
        scoreboard.StartGame();
    }

    public void GameOver()
    {
        inGame = false;
        foreach (GameObject pancake in pancakes)
        {
            Destroy(pancake);
        }
    }
}
