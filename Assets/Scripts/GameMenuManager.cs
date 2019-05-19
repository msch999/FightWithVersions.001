using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public GameObject gamePlay;
    public GameObject mainUI;


    // Start is called before the first frame update
    void Start()
    {
        gamePlay.SetActive(false);
    }

    public void HandlePointerClick()
    {
        Debug.Log("It's me, HandlePointerClick");
        gamePlay.SetActive(true);
        mainUI.SetActive(false);
    }

}
