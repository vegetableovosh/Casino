using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBet : MonoBehaviour
{
    public GameObject Button_Bet, Button_Stop;
    public IntefaceGame GameController;

    private void init()
    {
        GameController = GameObject.Find("GameController").GetComponent<IntefaceGame>();
    }

    public void Stop()
    {
        init();
        GameController.BetSum();
        GameController.StopGame();
        Button_Stop.SetActive(false);
        Button_Bet.SetActive(true);
    }

    public void StopwithMines()
    {
        init();
        Button_Stop.SetActive(false);
        Button_Bet.SetActive(true);
    }


}
