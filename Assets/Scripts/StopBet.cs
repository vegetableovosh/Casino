using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopBet : MonoBehaviour
{
    public GameObject Button_Bet, Button_Stop;
    public IntefaceGame GameController;
    private TMP_InputField input;

    private void init()
    {
        input = GameObject.Find("InputField_Bet").GetComponent<TMP_InputField>();
        GameController = GameObject.Find("GameController").GetComponent<IntefaceGame>();
    }

    public void Stop()
    {
        init();
        GameController.BetSum();
        GameController.StopGame();
        Button_Stop.SetActive(false);
        Button_Bet.SetActive(true);
        input.interactable = true;
    }

    public void StopwithMines()
    {
        init();
        Button_Stop.SetActive(false);
        Button_Bet.SetActive(true);
    }


}
